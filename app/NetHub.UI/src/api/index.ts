import axios, { AxiosError } from "axios";
import { getOrRefreshAccessToken } from "../utils/JwtService";
import { ApiError } from "../types/ApiError";
import {
  ArticleLocalizationsApi,
  ArticlesApi,
  CurrencyApi,
  CurrentUserApi,
  JwtApi, LanguagesApi, ResourcesApi,
  UsersApi
} from "./_api";

export const baseApiUrl = 'https://api.nethub.local:9010';

export const _api = axios.create({
  withCredentials: true
});

_api.interceptors.request.use(async (config) => {
  const accessToken = await getOrRefreshAccessToken(config.url);

  if (accessToken !== null)
    config.headers = {
      Authorization: 'Bearer ' + accessToken,
    };
  return config;
});

_api.interceptors.response.use(async (config) => {
    return config;
  },
  (error: AxiosError) => {
    throw new ApiError(error.message, error.response?.status);
  });

export default {
  _articlesApi: new ArticlesApi(baseApiUrl, _api),
  _localizationsApi: new ArticleLocalizationsApi(baseApiUrl, _api),
  _usersApi: new UsersApi(baseApiUrl, _api),
  _jwtApi: new JwtApi(baseApiUrl, _api),
  _currenciesApi: new CurrencyApi(baseApiUrl, _api),
  _currentUserApi: new CurrentUserApi(baseApiUrl, _api),
  _languagesApi: new LanguagesApi(baseApiUrl, _api),
  _resourcesApi: new ResourcesApi(baseApiUrl, _api)
}