import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ACCOUNT_CONTROLLER, ADD_ACCOUNT_ENDPOINT, API_BASE_URL, GET_ACCOUNT_ENDPOINT } from '../common/apiConstants';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  private loggedInSubject = new BehaviorSubject<boolean>(false);
  loggedIn$ = this.loggedInSubject.asObservable();

  getAccount(username: string): Observable<any> {
    const queryParams = {
      userName: username
    }
    let params = new HttpParams();
      Object.keys(queryParams).forEach((key) => {
      params = params.append(key, queryParams[key as keyof typeof queryParams]);
    });
    return this.http.get(API_BASE_URL + ACCOUNT_CONTROLLER + GET_ACCOUNT_ENDPOINT, { params });
  }

  addAccount(request: any): Observable<any> {
    return this.http.post(API_BASE_URL + ACCOUNT_CONTROLLER + ADD_ACCOUNT_ENDPOINT, request);
  }

  loginUser(){
    this.loggedInSubject.next(true);
  }

  logoutUser(){
    this.loggedInSubject.next(false);
  }
}
