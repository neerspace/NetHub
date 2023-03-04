import { JWTStorage } from './localStorageProvider';

export class JwtHelper {

  public static isAccessTokenValid(){
    const token = JWTStorage.getAccessToken();

    return !!token && !this.isAccessTokenExpired();
  }

  public static isAccessTokenExpired() {
    const expirationDate = JWTStorage.getAccessTokenExpires();
    return !expirationDate || new Date(expirationDate) < new Date();
  }

  public static isRefreshTokenExpired() {
    const expirationDate = JWTStorage.getRefreshTokenExpires();
    return !expirationDate || new Date(expirationDate) < new Date();
  }
}