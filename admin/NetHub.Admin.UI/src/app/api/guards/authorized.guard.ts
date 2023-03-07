import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  RouterStateSnapshot,
} from '@angular/router';
import { Injectable } from '@angular/core';
import { UserService } from '../../services/user.service';
import { logger } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable()
export class AuthorizedGuard implements CanActivate {
  constructor(private userService: UserService) {
  }

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
