import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { DateTime } from 'luxon';
import { map, Observable, of, Subscription } from 'rxjs';
import { logger } from '../../../environments/environment';
import { JWTApi, JwtResult, UserModel, UsersApi } from '../../api';
import { UnauthorizedError } from '../../shared/errors';
import { IJwtPayload } from '../../shared/types';
import { SecuredStorage } from '../storage';

@Injectable({ providedIn: 'root' })
export class UserService {
  private jwtPayload?: IJwtPayload;
  private lastToken?: string;
  private userData?: UserModel;

  constructor(
    private router: Router,
    private jwtApi: JWTApi,
    private usersApi: UsersApi,
    private securedStorage: SecuredStorage,
  ) {}

  get jwt() {
    const token = this.securedStorage.jwtPayload?.token;
    if (!token) {
      throw new UnauthorizedError();
    }
    if (this.lastToken === token) {
      this.jwtPayload = this.parseToken(token);
      this.lastToken = token;
    }
    if (!this.jwtPayload) {
      throw new UnauthorizedError();
    }
    return this.jwtPayload;
  }

  get user(): UserModel {
    if (!this.userData) {
      throw new UnauthorizedError();
    }
    return this.userData;
  }

  get isAuthorized(): boolean {
    const authTokenExp = this.securedStorage.jwtPayload?.tokenExpires;
    return !!authTokenExp && authTokenExp > DateTime.now();
  }

  loadUserInfo(): Subscription {
    logger.debug(this.securedStorage.jwtPayload!.token);
    return this.usersApi.me().subscribe({
      next: (result: UserModel) => {
        this.userData = result;
      },
      error: () => {
        logger.warn('User is not authorized');
      },
    });
  }

  setUserData(authResult: JwtResult | null): void {
    // console.log('token payload:', jwtDecode(authResult.token));
    this.securedStorage.jwtPayload = authResult;
  }

  refresh(redirectUrl: string): Observable<boolean> {
    const refreshExpires = this.securedStorage.jwtPayload?.refreshTokenExpires;
    if (this.securedStorage.jwtPayload && !refreshExpires) {
      this.router.navigateByUrl('/login');
      return of(false);
    }

    return this.jwtApi.refresh().pipe(
      map((result: JwtResult) => {
        if (result) {
          logger.debug('guard refresh');
          this.setUserData(result);
          return true;
        } else {
          logger.debug('guard failed');
          this.securedStorage.jwtPayload = null;
          // not logged in so redirect to login page
          this.router.navigateByUrl('/login', { state: { redirect: redirectUrl } });
          return false;
        }
      }),
    );
  }

  private parseToken(token: string): IJwtPayload {
    logger.debug('Token: ', token);
    const data: any = jwtDecode(token);
    logger.debug('Token payload:', data);
    return {
      id: data.sub,
      username: data.uniquename,
      role: data.role,
      permissions: data.perm,
    };
  }
}
