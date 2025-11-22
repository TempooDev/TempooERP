import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of } from 'rxjs';
import { ProductDto } from './product.dto';
import { Result } from '../result';
import { PagedList } from '../PagedList';
import { CreateProductCommand } from './createproduct.dto';
import { UpdateProductCommand } from './update-product.dto';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/catalog/products';

  getProducts(
    params?: Record<string, any>,
  ): Observable<Result<PagedList<ProductDto>>> {
    return this.http
      .get<Result<PagedList<ProductDto>>>(this.baseUrl, { params })
      .pipe(
        map(
          (res) =>
            new Result<PagedList<ProductDto>>({
              success: res.success,
              data: res.data,
              message: res.message,
            }),
        ),
        catchError(() =>
          of(
            new Result<PagedList<ProductDto>>({
              success: false,
              message: 'Failed to fetch products',
              errors: {},
            }),
          ),
        ),
      );
  }

  createProduct(
    createDto: CreateProductCommand,
  ): Observable<Result<void | undefined>> {
    return this.http
      .post<Result<void | undefined>>(this.baseUrl, createDto)
      .pipe(
        map(
          (res) =>
            new Result<void>({
              success: res.success,
              message: res.message,
            }),
        ),
        catchError(() =>
          of(
            new Result<void | undefined>({
              success: false,
              message: 'Failed to create product',
              errors: {},
            }),
          ),
        ),
      );
  }

  updateProduct(
    updateDto: UpdateProductCommand,
  ): Observable<Result<void | undefined>> {
    return this.http
      .put<
        Result<void | undefined>
      >(`${this.baseUrl}/${updateDto.id}`, updateDto, { observe: 'response' })
      .pipe(
        map((res) => {
          if (res.status === 204 || res.status === 200) {
            return new Result<void>({
              success: true,
              message: 'Product updated successfully',
            });
          }
          return new Result<void>({
            success: false,
            message: 'Unexpected response',
            errors: {},
          });
        }),
        catchError(() =>
          of(
            new Result<void | undefined>({
              success: false,
              message: 'Failed to delete product',
              errors: {},
            }),
          ),
        ),
      );
  }

  deleteProduct(id: string): Observable<Result<void | undefined>> {
    return this.http
      .delete(`${this.baseUrl}/${id}`, { observe: 'response' })
      .pipe(
        map((response) => {
          if (response.status === 204 || response.status === 200) {
            return new Result<void>({
              success: true,
              message: 'Product deleted successfully',
            });
          }
          return new Result<void>({
            success: false,
            message: 'Unexpected response',
            errors: {},
          });
        }),
        catchError(() =>
          of(
            new Result<void | undefined>({
              success: false,
              message: 'Failed to delete product',
              errors: {},
            }),
          ),
        ),
      );
  }

  getProductById(id: string): Observable<Result<ProductDto>> {
    return this.http.get<Result<ProductDto>>(`${this.baseUrl}/${id}`).pipe(
      map(
        (res) =>
          new Result<ProductDto>({
            success: res.success,
            data: res.data,
            message: res.message,
          }),
      ),
      catchError(() =>
        of(
          new Result<ProductDto>({
            success: false,
            message: 'Failed to fetch product',
            errors: {},
          }),
        ),
      ),
    );
  }
}
