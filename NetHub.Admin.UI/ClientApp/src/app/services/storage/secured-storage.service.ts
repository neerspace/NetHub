import { Injectable } from '@angular/core';
import { DateTime } from 'luxon';
import { StorageBase } from './storage-base';
import { IJwtData, StorageKey } from './types';

@Injectable({ providedIn: 'root' })
export class SecuredStorage extends StorageBase {
  constructor() {
    super();
  }

  get jwtPayload(): IJwtData | null {
    const storedValue = this.getItem(StorageKey.UserData);
    if (storedValue) {
      const json = JSON.parse(storedValue);
      return {
        username: this.userName!,
        token: json.at,
        tokenExpires: DateTime.fromSeconds(+json.at_exp),
        refreshTokenExpires: DateTime.fromSeconds(+json.rt_exp),
      };
    }
    return null;
  }

  set jwtPayload(result: IJwtData | null) {
    if (!result) {
      this.userName = null;
      this.setItem(StorageKey.UserData, null);
      return;
    }

    this.userName = result.username!;
    const json = {
      at: result.token,
      at_exp: result.tokenExpires.toSeconds().toString(),
      rt_exp: result.refreshTokenExpires.toSeconds().toString(),
    };

    this.setItem(StorageKey.UserData, JSON.stringify(json));
  }

  get userName(): string | null {
    return this.getItem(StorageKey.UserName);
  }

  private set userName(value: string | null) {
    this.setItem(StorageKey.UserName, value);
  }
}
