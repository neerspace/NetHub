// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiBaseUrl: 'https://localhost:9100',
};

export const logger = {
  debug: (m: string, ...optionalParams: any[]) => {
    console.log(m, ...optionalParams);
  },
  warn: (m: string, ...optionalParams: any[]) => {
    console.warn(m, ...optionalParams);
  },
  error: (m: string, ...optionalParams: any[]) => {
    console.error(m, ...optionalParams);
  },
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
