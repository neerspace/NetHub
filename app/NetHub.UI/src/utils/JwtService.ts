import { JWTStorage } from './localStorageProvider';
import { _jwtApi } from "../api";
import { JwtHelper } from "./JwtHelper";

async function waitIfIsRefreshing() {
  if (window.isRefreshing) {
    while (window.isRefreshing)
      await new Promise(resolve => setTimeout(resolve, 300));
  }
}

// gets a valid access token or refreshes it and gets new one
export async function getOrRefreshAccessToken(): Promise<string | null> {
  await waitIfIsRefreshing();

  const accessToken = JWTStorage.getAccessToken();

  if (accessToken && !JwtHelper.isAccessTokenExpired()) {
    return accessToken;
  }

  return await refreshToken();
}

async function refreshToken(): Promise<string | null> {
  if (window.isRefreshing) {
    await waitIfIsRefreshing();
    return JWTStorage.getAccessToken() || null;
  }
  window.isRefreshing = true;
  try {
    const jwt = await _jwtApi.refresh();
    JWTStorage.setTokensData(jwt);
    return jwt.token;
  } catch (e) {
    return null;
  } finally {
    setTimeout(() => window.isRefreshing = false, 300);
  }
}