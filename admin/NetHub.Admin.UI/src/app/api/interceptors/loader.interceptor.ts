import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { finalize, Observable } from 'rxjs';
import { LoaderService } from 'app/services/viewport';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
  constructor(private readonly loader: LoaderService) {}

  /**
   * Shows and hides loader bar
   * @param {HttpRequest<any>} request
   * @param {HttpHandler} next
   * @returns {Observable<HttpEvent<any>>}
   * @memberof TokenInterceptor
   */
  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loader.show();

    return next.handle(request).pipe(
      finalize(() => {
        this.loader.hide();
      }),
    );
  }
}
