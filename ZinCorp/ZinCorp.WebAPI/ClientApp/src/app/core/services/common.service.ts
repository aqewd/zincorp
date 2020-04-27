import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private baseUrl = '/api/common';

  constructor(private http: HttpClient) {}

  get() {
    return this.http.get(this.baseUrl).subscribe(
      (data) => console.log('Logged in successfully')
    );;
  }
}
