import { Injectable } from '@angular/core';
import { Theme } from 'app/theme/types';
import { StorageBase } from 'neercms/services/storage';
import { SettingsKey, StorageKey } from './types';

@Injectable({ providedIn: 'root' })
export class StorageService extends StorageBase {
  set(key: SettingsKey | string, value: any | null) {
    let settings = this.getItem(StorageKey.Settings);
    const json = settings ? JSON.parse(settings) : {};
    json[key] = value;
    settings = JSON.stringify(json);
    this.setItem(StorageKey.Settings, settings);
  }

  get(key: SettingsKey | string): any | null {
    let settings = this.getItem(StorageKey.Settings);
    return settings ? JSON.parse(settings)[key] : null;
  }

  setColumnSequence(sequenceName: string, sequence: string[] | null) {
    this.set(SettingsKey.ColumnSequence + sequenceName, sequence);
  }

  getColumnSequence(sequenceName: string): string[] | null {
    return this.get(SettingsKey.ColumnSequence + sequenceName);
  }

  get language(): string | null {
    return this.getItem(StorageKey.Language);
  }

  set language(value: string | null) {
    this.setItem(StorageKey.Language, value);
  }

  get theme(): Theme | null {
    return this.getItem(StorageKey.Theme) as Theme;
  }

  set theme(value: Theme | null) {
    this.setItem(StorageKey.Theme, value);
  }
}
