import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DateTime } from 'luxon';
import { BehaviorSubject, filter, first, Observable, switchMap, tap } from 'rxjs';
import { SecuredStorage } from '../services/storage';
import { IJwtData } from '../services/storage/types';
import { JWTApi } from './index';

@Injectable({ providedIn: 'root' })
export class RequestTokenService {
  private tokenRefreshInProgress: boolean = false;
  private refreshAccessTokenSubject = new BehaviorSubject<IJwtData | null>(null);

  constructor(private jwtApi: JWTApi, private securedStorage: SecuredStorage) {}

  public handleRequest(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwt = this.securedStorage.jwtPayload;

    if (!jwt || this.isTokenExpired(jwt)) {
      console.log('[JWT] Token expired or not exists');
      return this.refreshToken().pipe(
        switchMap(token => {
          request = this.appendBearer(token, request);
          return next.handle(request);
        }),
      );
    }
    if (this.isAwaitingForRefresh(jwt)) {
      console.log('[JWT]  Awaiting refresh');
      return this.waitForRefreshToken().pipe(
        switchMap((jwt: IJwtData | null) => {
          request = this.appendBearer(jwt!, request);
          return next.handle(request);
        }),
      );
    }

    // console.log('[JWT] Append bearer');
    request = this.appendBearer(jwt, request);
    return next.handle(request);
  }

  private isTokenExpired(jwt: IJwtData): boolean {
    return jwt.tokenExpires < DateTime.utc() && !this.tokenRefreshInProgress;
  }

  private isAwaitingForRefresh(jwt: IJwtData): boolean {
    return jwt.tokenExpires < DateTime.utc() && this.tokenRefreshInProgress;
  }

  // Completes after first event
  private refreshToken(): Observable<IJwtData> {
    return this.jwtApi.refresh().pipe(
      tap((token: IJwtData) => {
        this.securedStorage.jwtPayload = token;
        this.tokenRefreshInProgress = false;
        this.refreshAccessTokenSubject.next(token);
      }),
    );
  }

  // Completes after first event
  private waitForRefreshToken(): Observable<IJwtData | null> {
    return this.refreshAccessTokenSubject.pipe(
      filter(result => result !== null),
      first(),
    );
  }

  private appendBearer(tokenData: IJwtData, request: HttpRequest<any>): HttpRequest<any> {
    request = request.clone({
      setHeaders: {
        // 'Content-Type': request.body instanceof FormData ? 'multipart/form-data' : 'application/json',
        Authorization: 'Bearer ' + tokenData.token,
      },
    });
    return request;
  }
}
