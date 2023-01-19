import { StorageKey } from './types';

export abstract class StorageBase {
  protected getItem(key: StorageKey): string | null {
    return window.localStorage.getItem(key);
  }

  protected setItem(key: StorageKey, value: string | null) {
    if (!value) {
      window.localStorage.removeItem(key);
    } else {
      window.localStorage.setItem(key, value);
    }
  }
}
