import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SignUpResponse } from '../../models/sign-up/sign-up-response.model';
import { environment } from '../../../../environments/environment';
import { SignUpRequest } from '../../models/sign-up/sign-up-request.model';

@Injectable({
  providedIn: 'root'
})
export class SignUpService {

  constructor(private httpClient: HttpClient) { }

  public signUp = (body: SignUpRequest) => {
    return this.httpClient.post<SignUpResponse>(`${environment.apiUrl}users`, body);
  }  
}
