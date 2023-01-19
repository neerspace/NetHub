export const environment = {
  production: true,
  apiBaseUrl: 'https://admin-api.nethub.net',
};

export const logger = {
  debug: (m: string, ...optionalParams: any[]) => {
    // ignore
  },
  warn: (m: string, ...optionalParams: any[]) => {
    console.warn(m, ...optionalParams);
  },
  error: (m: string, ...optionalParams: any[]) => {
    console.error(m, ...optionalParams);
  },
};
