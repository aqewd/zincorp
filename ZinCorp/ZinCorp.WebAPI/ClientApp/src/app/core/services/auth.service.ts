import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { NzNotificationService } from 'ng-zorro-antd';
import { BaseService } from './base.service';
import * as jwt_decode from 'jwt-decode';

export const TOKEN_NAME = 'zinCorp_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  private baseUrl = '/api/auth';
  private loggedIn: boolean = false;
  private role: 'Customer' | 'Admin' = 'Customer';

  constructor(private http: HttpClient, public notification: NzNotificationService) {
    super(notification);
    this.role = localStorage.getItem('role') as 'Customer'| 'Admin';
  }

  auth(userName: string, password: string) {
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    const credential = { userName, password };
    return this.http.post(this.baseUrl, credential, { headers }).pipe(
      tap((res) => {
        localStorage.setItem(TOKEN_NAME, res[TOKEN_NAME]);
        localStorage.setItem('role', res['role']);
        this.loggedIn = true;
      }),
      catchError(this.handleError('auth', null))
    );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_NAME);
    localStorage.removeItem('role');
    this.loggedIn = false;
  }

  get isLoggedIn(): boolean {
    return this.loggedIn && !this.isTokenExpired();
  }

  get isLoggedLikeAdmin(): boolean {
    return this.isLoggedIn && this.isAdmin;
  }

  get isAdmin(): boolean {
    return this.role === 'Admin';
  }

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);

    if (decoded.exp === undefined) {
      return null;
    }

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(): boolean {
    const token = this.getToken();
    if (!token) {
      return true;
    }

    const date = this.getTokenExpirationDate(token);
    if (date === undefined) {
      return false;
    }
    return !(date.valueOf() > new Date().valueOf());
  }

  getToken(): string {
    return localStorage.getItem(TOKEN_NAME);
  }
}
