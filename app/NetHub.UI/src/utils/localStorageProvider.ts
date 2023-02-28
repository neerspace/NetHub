import { JwtResult } from "../api/_api";

export class JWTStorage {
  static getAccessToken() {
    return localStorage.getItem('accessToken');
  }

  static getAccessTokenExpires() {
    const expiresDate = localStorage.getItem('accessTokenExpires');

    if (expiresDate)
      return new Date(expiresDate);

    return null;
  }

  static getRefreshTokenExpires() {
    return localStorage.getItem('refreshTokenExpires');
  }

  static setTokensData(data: JwtResult) {
    localStorage.setItem('accessToken', data.token);
    localStorage.setItem('accessTokenExpires', data.tokenExpires.toString());
    localStorage.setItem('refreshTokenExpires', data.refreshTokenExpires.toString());
  }

  static clearTokensData() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('accessTokenExpires');
    localStorage.removeItem('refreshTokenExpires');
  }
}

export class ArticleStorage {
  static setTitle(value: string) {
    localStorage.setItem('title', value);
  }

  static setDescription(value: string) {
    localStorage.setItem('description', value);
  }

  static setHtml(value: string) {
    localStorage.setItem('html', value);
  }

  static setTags(value: string) {
    localStorage.setItem('tags', value);
  }

  static setLink(value: string) {
    localStorage.setItem('link', value);
  }

  static getTitle() {
    return localStorage.getItem('title');
  }

  static getDescription() {
    return localStorage.getItem('description');
  }

  static getHtml() {
    return localStorage.getItem('html');
  }

  static getTags() {
    return localStorage.getItem('tags');
  }

  static getLink() {
    return localStorage.getItem('link');
  }

  static clearArticleData() {
    localStorage.removeItem('title');
    localStorage.removeItem('description');
    localStorage.removeItem('html');
    localStorage.removeItem('tags');
    localStorage.removeItem('link');
  }
}

