import {JWTStorage} from "./localStorageProvider";

export function isAccessTokenValid() {
  return JWTStorage.getAccessToken() && new Date(JWTStorage.getAccessTokenExpires()!) > new Date();
}

export function isAccessTokenExpired() {
  return JWTStorage.getAccessToken() && new Date(JWTStorage.getAccessTokenExpires()!) < new Date();
}

export function isRefreshTokenValid() {
  return new Date(JWTStorage.getRefreshTokenExpires()!) > new Date();
}