import { Theme, Themes } from './types';

export const themes: Themes = {
  [Theme.Dark]: {
    className: 'dark-theme',
    icon: 'dark_mode',
    isDark: true,
  },
  [Theme.Light]: {
    className: 'light-theme',
    icon: 'light_mode',
    isDark: false,
  },
};
