import { HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { TokenInterceptor } from './auth/token.interceptor';
import { HttpErrorsInterceptor } from './errors/http-errors.interceptor';

export const interceptorsProvider = [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorsInterceptor, multi: true }
]