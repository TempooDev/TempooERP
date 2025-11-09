import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of } from 'rxjs';
import { ProductDto } from './produtc.dto';
import { Result } from '../result';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/catalog/products';

  getProducts(): Observable<Result<ProductDto[]>> {
    return this.http.get<ProductDto[]>(this.baseUrl).pipe(
      map(res => new Result<ProductDto[]>({ success: true, data: res })),
      catchError(() => of(new Result<ProductDto[]>({ success: false, error: 'Failed to fetch products' })))
    );
  }
}
