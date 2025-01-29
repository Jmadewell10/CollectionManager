import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ACCOUNT_CONTROLLER, API_BASE_URL, AUTHENTICATE_ENDPOINT } from '../common/apiConstants';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    const loginData = { username, password };
    return this.http.post(API_BASE_URL + ACCOUNT_CONTROLLER + AUTHENTICATE_ENDPOINT, loginData);
  }
}
