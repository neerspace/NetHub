import { JWTStorage } from './localStorageProvider';
import { _jwtApi } from "../api";

const refreshTokenCookie = 'NetHub-Refresh-Token';

async function waitIfIsRefreshing() {
  if (window.isRefreshing) {
    console.log('[JWT] is refreshing = true, awaiting... [R] - ' + window.isRefreshing);
    while (window.isRefreshing)
      await new Promise(resolve => setTimeout(resolve, 300));
  }
}

// gets a valid access token or refreshes it and gets new one
export async function getOrRefreshAccessToken(): Promise<string | null> {
    await waitIfIsRefreshing();

    const accessToken = JWTStorage.getAccessToken();
    const expirationDate = JWTStorage.getAccessTokenExpires();

    if (accessToken && expirationDate && expirationDate > new Date()) {
      return accessToken;
    }

    if (isRefreshTokenCookieExist()) {
      return await refreshToken();
    }

    return null;
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
    const jwt = await _jwtApi.refresh();
    JWTStorage.setTokensData(jwt);
    console.log('[JWT] token refreshed');
    return jwt.token;
  } catch (e) {
    console.log('token did not refresh', e);
    // JWTStorage.clearTokensData();
    // window.location.href = '/login';
    return null;
  } finally {
    setTimeout(() => window.isRefreshing = false, 300);
  }
}

function isAccessTokenExpired() {
  const expirationDate = JWTStorage.getAccessTokenExpires();
  return !expirationDate || new Date(expirationDate) < new Date();
}

function isRefreshTokenCookieExist() {
  const d = new Date();
  d.setTime(d.getTime() + (1000));
  const expires = 'expires=' + d.toUTCString();

  document.cookie = refreshTokenCookie + '=check;path=/;' + expires + ';SameSite=Strict;domain=.nethub.local';
  return document.cookie.indexOf(refreshTokenCookie + '=') == -1;
}