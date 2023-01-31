export enum Theme {
  Dark = 'dark',
  Light = 'light',
}

export interface IThemeInfo {
  className: string;
  icon: string;
  isDark: boolean;
}

export type Themes = {
  [key in Theme]: IThemeInfo;
};
