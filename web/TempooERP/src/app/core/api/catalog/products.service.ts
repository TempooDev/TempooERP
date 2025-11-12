import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of } from 'rxjs';
import { ProductDto } from './product.dto';
import { Result } from '../result';
import { PagedList } from '../PagedList';
import { CreateProductCommand } from './createproduct.dto';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/catalog/products';

  getProducts(params?: Record<string, any>): Observable<Result<PagedList<ProductDto>>> {
    return this.http.get<Result<PagedList<ProductDto>>>(this.baseUrl, { params }).pipe(
      map(res => new Result<PagedList<ProductDto>>({
        success: res.success,
        data: res.data,
        message: res.message,
      })),
        catchError(() =>
          of(new Result<PagedList<ProductDto>>({
            success: false,
            message: 'Failed to fetch products',
            errors: {}
          }))
        )
      );
    }

    createProduct(createDto: CreateProductCommand): Observable<Result<void|undefined>> {
      return this.http.post<Result<void|undefined>>(this.baseUrl, createDto).pipe(
        map(res => new Result<void>({
          success: res.success,
          message: res.message,
        })),
        catchError(() =>
          of(new Result<void|undefined>({
            success: false,
            message: 'Failed to create product',
            errors: {}
          }))
        )
      );
  }
}
