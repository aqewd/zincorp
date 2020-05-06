import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { NzNotificationService } from 'ng-zorro-antd';
import { Customer } from '../../shared/interfaces/customer';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CustomersService extends BaseService {
  private baseUrl = '/api/customers';

  constructor(private http: HttpClient, public notification: NzNotificationService) {
    super(notification);
  }

  get(): Observable<Customer[]> {
    return this.http
      .get<Customer[]>(this.baseUrl)
      .pipe(tap(), catchError(this.handleError('getCustomerList', [])));
  }
}
