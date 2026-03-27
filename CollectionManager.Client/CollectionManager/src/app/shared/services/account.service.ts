import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, of, tap } from 'rxjs';
import { ACCOUNT_CONTROLLER, ADD_ACCOUNT_ENDPOINT, API_BASE_URL, CHECK_TOKEN_ENDPOINT, GET_ACCOUNT_ENDPOINT } from '../common/apiConstants';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient, private router: Router) { }

  private loggedInSubject = new BehaviorSubject<boolean>(!!localStorage.getItem('token'));
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
    this.router.navigate(['/home']);
  }

  logoutUser(){
    this.loggedInSubject.next(false);
    localStorage.removeItem('token');
  }

  checkToken(): Observable<any> {
  const token = localStorage.getItem('token');
  if (!token) return of(false);

  return this.http.get<{ token: string }>(API_BASE_URL + ACCOUNT_CONTROLLER + CHECK_TOKEN_ENDPOINT).pipe(
    tap(response => {
      localStorage.setItem('token', response.token);
      this.loginUser();
    }),
    catchError(() => {
      this.logoutUser();
      return of(false);
    })
  );
}
}
