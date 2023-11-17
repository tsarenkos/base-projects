import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthFacade } from '../../../core/facades/auth/auth-facade.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private authFacade: AuthFacade) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const accessToken = this.authFacade.getAccessToken();

    if (accessToken) {
      request = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${accessToken}`)
      });
    }

    return next.handle(request);
  }
}
