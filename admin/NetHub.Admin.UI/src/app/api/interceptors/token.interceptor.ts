import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, tap, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { SecuredStorage } from '../../services/storage';
import { LoaderService } from '../../services/viewport';
import { JWTApi } from '../index';
import { RequestTokenService } from '../request-token.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private jwtApi: JWTApi,
    private router: Router,
    private route: ActivatedRoute,
    private loader: LoaderService,
    private securedStorage: SecuredStorage,
    private tokenService: RequestTokenService,
  ) {}

  /**
   * Intercept all HTTP request to add JWT token to Headers
   * @param {HttpRequest<any>} request
   * @param {HttpHandler} next
   * @returns {Observable<HttpEvent<any>>}
   * @memberof TokenInterceptor
   */
  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loader.show();
    console.log('token start');

    if (this.isSecureUrl(request.url)) {
      console.log('secured case');
      return this.tokenService.handleRequest(request, next).pipe(tap(_ => this.loader.hide()));
    }

    console.log('alt case');
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
              this.router.navigateByUrl('/login', { state: { redirect: this.route.url } });
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
