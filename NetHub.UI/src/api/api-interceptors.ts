import { AxiosRequestConfig } from 'axios';


export async function jwtRequestInterceptor(config: AxiosRequestConfig): Promise<AxiosRequestConfig> {
  if (isUrlAuthorized(config.url!)) {

  }
  return config;
}

function isUrlAuthorized(url: string): boolean {
  console.log('url', url);
  return false;
}
