import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JWTApi } from 'app/api';
import { RequestTokenService } from 'app/api/request-token.service';
import { environment } from 'app/environments/environment';
import { SecuredStorage } from 'app/services/storage';
import { LoaderService } from 'neercms/services/viewport';

import { Observable, tap, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private readonly jwtApi: JWTApi,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly loader: LoaderService,
    private readonly securedStorage: SecuredStorage,
    private readonly tokenService: RequestTokenService,
  ) {}

  /**
   * Intercept all HTTP request to add JWT token to Headers
   * @param {HttpRequest<any>} request
   * @param {HttpHandler} next
   * @returns {Observable<HttpEvent<any>>}
   * @memberof TokenInterceptor
   */
  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // console.log('[JWT] Token start');

    if (this.isSecureUrl(request.url)) {
      // console.log('[JWT] Secured case');
      return this.tokenService.handleRequest(request, next);
    }

    // console.log('[JWT] Alt case');
    return this.invokeRequest(next, request);
  }

  private isSecureUrl(url: string): boolean {
    return url.startsWith(environment.apiBaseUrl + '/') && !url.includes('/jwt');
  }

  private invokeRequest(next: HttpHandler, request: HttpRequest<any>): Observable<HttpEvent<any>> {
    return next
      .handle(request)
      .pipe(tap(_ => this.loader.hide()))
      .pipe(
        catchError(error => {
          if (error instanceof HttpErrorResponse) {
            if (request.url.endsWith('/jwt/refresh')) {
              this.securedStorage.jwtPayload = null;
              // this.router.navigateByUrl('/login', { state: { redirect: this.route.url } });
            } else if (error.status === 403) {
              this.router.navigateByUrl('/error/403', { skipLocationChange: true });
            } else if (error.status === 401) {
              const observer = this.tokenService.handleRequest(request, next);
              if (observer) {
                return observer;
              }
            }
          }

          return throwError(error);
        }),
      );
  }
}
