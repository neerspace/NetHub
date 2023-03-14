import axios, { AxiosError } from "axios";
import { getOrRefreshAccessToken } from "../utils/JwtService";
import { ApiError } from "../types/ApiError";
import {
  ArticlesApi,
  ArticleSetsApi,
  CurrencyApi,
  CurrentUserApi,
  JwtApi, LanguagesApi,
  MyArticlesApi, ResourcesApi,
  UsersApi
} from "./_api";

export const baseApiUrl = 'https://api.nethub.local:9010';

const _apiInstance = axios.create(
  {
    headers:{
      'Content-Type': 'application/json'
    },
    //DO NOT EVER TOUCH THIS
    transformRequest: (data, headers) => {
      if (headers && typeof data == 'string')
        headers['Content-Type'] = 'application/json';

      return data;
    }
  }
);

const _jwtApiInstance = axios.create({
  withCredentials: true,
});

_apiInstance.interceptors.request.use(async (config) => {
  const accessToken = await getOrRefreshAccessToken();

  if (accessToken !== null)
    config.headers = {
      Authorization: 'Bearer ' + accessToken,
    };

  return config;
});

_apiInstance.interceptors.response.use(async (config) => {
    return config;
  },
  (error: AxiosError) => {
    throw new ApiError(error.message, error.response?.status);
  })
;

export const _articlesSetsApi = new ArticleSetsApi(baseApiUrl, _apiInstance);
export const _myArticlesApi = new MyArticlesApi(baseApiUrl, _apiInstance);
export const _articlesApi = new ArticlesApi(baseApiUrl, _apiInstance);
export const _usersApi = new UsersApi(baseApiUrl, _apiInstance);
export const _jwtApi = new JwtApi(baseApiUrl, _jwtApiInstance);
export const _currenciesApi = new CurrencyApi(baseApiUrl, _apiInstance);
export const _currentUserApi = new CurrentUserApi(baseApiUrl, _apiInstance);
export const _languagesApi = new LanguagesApi(baseApiUrl, _apiInstance);
export const _resourcesApi = new ResourcesApi(baseApiUrl, _apiInstance);