import { Injectable } from '@angular/core';
import packageInfo from '../../../package.json';

@Injectable({ providedIn: 'root' })
export class SettingsService {
  get applicationVersion(): string {
    return packageInfo.version;
  }
}
