import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { NzNotificationService } from 'ng-zorro-antd';
import { catchError, tap } from 'rxjs/operators';
import { Product } from '../../../shared/interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = '/api/products';

  constructor(private http: HttpClient, private notification: NzNotificationService) {}

  get() {
    return this.http
      .get<Product[]>(this.baseUrl)
      .pipe(tap(), catchError(this.handleError('getProductList', [])));
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
