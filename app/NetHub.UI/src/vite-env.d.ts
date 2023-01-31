/// <reference types="vite/client" />

export interface TinyConfig {
  plugins: string[];
  key: string;
  toolbar: string;
}

export type ILanguage = 'uk' | 'en';

declare global {
  interface Window {
    Telegram: {
      Login: {
        auth: (config: any, callback: (data: any) => void) => void;
      }
    },
    isRefreshing: boolean
  }
}
