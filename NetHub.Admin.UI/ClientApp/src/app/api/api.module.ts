import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { JWTApi, LanguagesApi, PermissionsApi, RolesApi, UsersApi } from './index';
import { TokenInterceptor } from './interceptors/token.interceptor';

@NgModule({
  imports: [CommonModule],
  providers: [
    // Interceptors
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },

    // App
    JWTApi,
    UsersApi,
    RolesApi,
    PermissionsApi,
    LanguagesApi,
  ],
})
export class ApiModule {}
