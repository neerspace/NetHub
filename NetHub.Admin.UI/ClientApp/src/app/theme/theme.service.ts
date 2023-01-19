import { MediaMatcher } from '@angular/cdk/layout';
import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { StorageService } from '../services/storage';
import { themes } from './themes';
import { IThemeInfo, Theme } from './types';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private overlayContainer: HTMLBodyElement;
  private darkBrowserTheme: MediaQueryList;

  constructor(
    @Inject(DOCUMENT) document: Document,
    media: MediaMatcher,
    private storage: StorageService,
  ) {
    this.darkBrowserTheme = media.matchMedia('(prefers-color-scheme: dark)');
    this.darkBrowserTheme.addEventListener('change', event => {
      console.log('User changed browser theme to', event.matches ? 'dark' : 'light');
      this.setTheme(this.defaultTheme);
    });

    this.overlayContainer = document.getElementsByTagName('body')[0] as HTMLBodyElement;
    this.refreshTheme();
  }

  private get defaultTheme(): Theme {
    if (this.darkBrowserTheme.matches) {
      return Theme.Dark;
    } else {
      return Theme.Light;
    }
  }

  get currentThemeName(): Theme {
    let theme: Theme | null = this.storage.theme;
    if (!theme) {
      this.storage.theme = this.defaultTheme;
      return this.defaultTheme;
    }
    return theme;
  }

  get currentTheme(): IThemeInfo {
    return themes[this.currentThemeName];
  }

  refreshTheme() {
    const storedTheme = this.storage.theme || Theme.Dark;
    if (storedTheme) {
      this.setTheme(storedTheme);
    }
  }

  toggleTheme(): void {
    let nextTheme: Theme;
    switch (this.currentThemeName) {
      case Theme.Dark:
        nextTheme = Theme.Light;
        break;
      case Theme.Light:
        nextTheme = Theme.Dark;
        break;
    }

    this.setTheme(nextTheme);
  }

  setTheme(theme: Theme) {
    let themeFound = false;
    for (const [themeName, themeInto] of Object.entries(themes)) {
      if (themeName === theme) {
        this.overlayContainer.classList.add(themeInto.className);
        themeFound = true;
      } else if (this.overlayContainer.classList.contains(themeInto.className)) {
        this.overlayContainer.classList.remove(themeInto.className);
      }
    }

    if (!themeFound) {
      this.overlayContainer.classList.add(this.currentTheme.className);
    }

    this.storage.theme = theme;
  }
}
