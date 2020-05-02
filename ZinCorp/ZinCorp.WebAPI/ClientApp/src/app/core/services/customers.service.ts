import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import {Observable, of} from 'rxjs';
import {NzNotificationService} from 'ng-zorro-antd';
import {Customer} from '../../shared/interfaces/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  private baseUrl = '/api/customers';

  constructor(private http: HttpClient, private notification: NzNotificationService) {}

  get(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseUrl).pipe(
      tap(),
      catchError(this.handleError('getCustomerList', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
      this.notification.error('Ошибка', error.message);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    console.log(message);
  }
}
