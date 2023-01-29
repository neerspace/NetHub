import { jwtApi } from '../api/api';
import { JWTStorage } from './localStorageProvider';


async function waitIfIsRefreshing() {
  if (window.isRefreshing) {
    console.log('[JWT] is refreshing = true, awaiting...');
    while (window.isRefreshing)
      await new Promise(resolve => setTimeout(resolve, 300));
  }
}

// gets a valid access token or refreshes it and gets new one
export async function getOrRefreshAccessToken(url?: string): Promise<string | null> {
  if (!url || isAuthorizedUrl(url!)) {
    await waitIfIsRefreshing();

    const accessToken = JWTStorage.getAccessToken();
    if (!accessToken) {
      console.log('[JWT] no access token');
      return null;
    }

    if (!isAccessTokenExpired()) {
      // console.log('[JWT] access token is valid');
      return accessToken;
    }

    return await refreshToken();
  }

  console.log('[JWT] access token skipped');
  return null;
}

// use this to force the refresh
export async function waitOrRefreshToken(): Promise<string | null> {
  await waitIfIsRefreshing();

  if (!isAccessTokenExpired()) {
    return JWTStorage.getAccessToken();
  }

  return await refreshToken();
}

async function refreshToken(): Promise<string | null> {
  if (window.isRefreshing) {
    console.log('[JWT] attempt to refresh while another refresh is running!');
    await waitIfIsRefreshing();
    return JWTStorage.getAccessToken() || null;
  }
  console.log('refreshing:', window.isRefreshing);
  window.isRefreshing = true;
  console.log('[JWT] refreshing...');
  try {
    const jwt = await jwtApi.refresh();
    JWTStorage.setTokensData(jwt);
    console.log('[JWT] token refreshed');
    return jwt.token;
  } catch (e) {
    console.log('token did not refresh', e);
    JWTStorage.clearTokensData();
    window.location.href = '/login';
    return null;
  } finally {
    setTimeout(() => window.isRefreshing = false, 300);
  }
}

function isAccessTokenExpired() {
  const expirationDate = JWTStorage.getAccessTokenExpires();
  return !expirationDate || new Date(expirationDate) < new Date();
}

function isAuthorizedUrl(url: string): boolean {
  return !url.startsWith('/jwt/');
}
