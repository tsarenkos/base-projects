import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { TokenResponse } from '../../models/auth/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  public login = (email: string, password: string) => {
    const url = `${environment.openIddict.uri}connect/token`;

    const body = new HttpParams()
      .set('grant_type', 'password')
      .set('username', email)
      .set('password', password)
      .set('scope', environment.openIddict.scope)
      .set('client_id', environment.openIddict.clientId)
      .set('client_secret', environment.openIddict.clientSecret)
      .toString();

    const headers = new HttpHeaders()
      .set('Content-Type', 'application/x-www-form-urlencoded');

    const options = { headers };

    return this.httpClient.post<TokenResponse>(url, body, options);
  }

  public refreshToken = (token: string) => {
    if (!token) {
      throw new Error("Refresh token hasn't been passed.");
    }

    const url = `${environment.openIddict.uri}connect/token`;

    const body = new HttpParams()
      .set('grant_type', 'refresh_token')
      .set('refresh_token', token)
      .set('client_id', environment.openIddict.clientId)
      .set('client_secret', environment.openIddict.clientSecret)
      .toString();

    const headers = new HttpHeaders()
      .set('Content-Type', 'application/x-www-form-urlencoded');

    const options = { headers };

    return this.httpClient.post<TokenResponse>(url, body, options);
  }
}