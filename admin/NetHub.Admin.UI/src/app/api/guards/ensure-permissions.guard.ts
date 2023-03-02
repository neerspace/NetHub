import { ActivatedRouteSnapshot, CanActivateChild, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

export class EnsurePermissionsGuard implements CanActivateChild {
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean> | boolean {
    // console.log('childRoute', childRoute);
    return true;
    // return this.canActivate(childRoute, state);
  }
}