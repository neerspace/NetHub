import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { logger } from 'app/environments/environment';
import { UserService } from 'app/services';
import { Observable } from 'rxjs';

@Injectable()
export class AuthorizedGuard implements CanActivate {
  constructor(private userService: UserService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean> | boolean {
    if (this.userService.isAuthorized) {
      logger.debug('guard passed');
      return true;
    } else {
      return this.userService.refresh(state.url);
    }
  }
}
