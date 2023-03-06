import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import {
  ArticlesApi,
  JWTApi,
  LanguagesApi,
  LocalizationsApi,
  PermissionsApi,
  RolesApi,
  UsersApi,
} from './index';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { TokenInterceptor } from './interceptors/token.interceptor';

@NgModule({
  imports: [CommonModule],
  providers: [
    // Interceptors
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },

    // App
    ArticlesApi,
    JWTApi,
    LanguagesApi,
    LocalizationsApi,
    PermissionsApi,
    RolesApi,
    UsersApi,
  ],
})
export class ApiModule {}
