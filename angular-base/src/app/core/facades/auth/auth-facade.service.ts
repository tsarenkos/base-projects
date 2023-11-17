import { Injectable } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { TokenResponse } from '../../models/auth/login-response.model';
import { tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthFacade {

  constructor(private authService: AuthService) { }

  public login = (email: string, password: string) => {
    return this.authService.login(email, password)
      .pipe(tap(this.saveTokens))
  }

  public getAccessToken = () => {
    return window.localStorage.getItem('access_token');
  }

  public refreshToken = () => {
    const refreshToken = window.localStorage.getItem('refresh_token');
    if (!refreshToken) {
      return throwError(() => new Error("Refresh token hasn't been received."));
    } else {
      return this.authService.refreshToken(refreshToken).pipe(
        tap(this.saveTokens)
      );
    }
  }

  public logout = () => {
    this.clearStorage();
  }

  private saveTokens = (tokens: TokenResponse) => {
    if (!tokens) {
      throw Error();
    }

    this.clearStorage();
    window.localStorage.setItem('access_token', tokens.access_token);
    window.localStorage.setItem('refresh_token', tokens.refresh_token);
  };

  private clearStorage = () => {
    window.localStorage.removeItem('access_token');
    window.localStorage.removeItem('refresh_token');
  };
}