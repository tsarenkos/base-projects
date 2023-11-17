import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, filter, finalize, switchMap, take, throwError } from 'rxjs';
import { AuthFacade } from '../../facades/auth/auth-facade.service';
import { Router } from '@angular/router';

@Injectable()
export class HttpErrorsInterceptor implements HttpInterceptor {
  isRefreshing = false;
  isTokenRefreshed$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private authFacade: AuthFacade, private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401) {
          return this.handle401Error(request, next);
        }
        return throwError(err);
      })
    );
  }

  private handle401Error(request: HttpRequest<unknown>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.isTokenRefreshed$.next(false);

      return this.authFacade.refreshToken().pipe(
        switchMap(() => {
          this.isRefreshing = false;
          this.isTokenRefreshed$.next(true);
          return next.handle(this.addTokenHeader(request));
        }),
        catchError((err) => {
          this.authFacade.logout();
          this.router.navigateByUrl('/auth/login');
          return throwError(err);
        }),
        finalize(() => {
          this.isRefreshing = false;
        }));
    } else {
      return this.isTokenRefreshed$.pipe(
        filter(Boolean),
        take(1),
        switchMap(() => {
          return next.handle(this.addTokenHeader(request));
        }));
    }
  }

  addTokenHeader(request: HttpRequest<unknown>): HttpRequest<unknown> {
    const accessToken = this.authFacade.getAccessToken();
    return accessToken
      ? request.clone({ headers: (request.headers ?? new HttpHeaders()).set('Authorization', `Bearer ${accessToken}`) })
      : request;
  }
}
