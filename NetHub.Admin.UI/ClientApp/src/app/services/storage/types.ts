import { DateTime } from 'luxon';

export enum StorageKey {
  UserData = 'usrdata',
  UserName = 'ursname',
  Language = 'lang',
  SidebarCollapsed = 'sidebar',
  Theme = 'theme',
  Settings = 'settings',
}

export enum SettingsKey {
  UsersSplitSizes = 'usr_sps',
}

export interface IJwtData {
  username?: string;
  token: string;
  tokenExpires: DateTime;
  refreshTokenExpires: DateTime;
}
