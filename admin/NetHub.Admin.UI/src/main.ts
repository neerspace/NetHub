import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { API_URL } from 'app/api';
import { AppModule } from 'app/app.module';
import { environment } from 'app/environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: API_URL, useValue: environment.apiBaseUrl },
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers)
  .bootstrapModule(AppModule)
  .catch(err => console.log(err));
