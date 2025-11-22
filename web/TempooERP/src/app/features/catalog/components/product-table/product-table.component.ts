import { CommonModule } from '@angular/common';
import { Component, computed, inject, input, signal } from '@angular/core';
import { ProductDto } from '../../../../core/api/catalog/product.dto';
import {
  PageChangeEvent,
  SortChangeEvent,
  TableColumn,
  TableComponent,
} from '../../../../shared/components/table/table';
import { ProductsService } from '../../../../core/api/catalog/products.service';
import { PagedList } from '../../../../core/api/PagedList';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs/internal/operators/switchMap';
import { catchError, map, of, tap } from 'rxjs';
import {
  TableActions,
  TableActionsEnum,
} from '../../../../shared/enums/table-actions';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-table',
  standalone: true,
  imports: [CommonModule, TableComponent],
  templateUrl: './product-table.component.html',
})
export class ProductTableComponent {
  private readonly productService = inject(ProductsService);
  private readonly router = inject(Router);
  private readonly page = signal(1);
  private readonly pageSize = signal(10);
  private readonly sortBy = signal<string | null>(null);
  private readonly sortDirection = signal<'asc' | 'desc'>('asc');
  private readonly products = signal<ProductDto[]>([]);
  private readonly _loading = signal(false);
  private readonly refresh = signal(0);

  readonly columns: TableColumn[] = [
    { field: 'name', header: 'Nombre', sortable: true },
    { field: 'price', header: 'Precio', sortable: true, align: 'right' },
    {
      field: 'isActive',
      header: 'Activo',
      sortable: true,
      align: 'center',
      width: '90px',
    },
  ];

  readonly actions: TableActions[] = [
    {
      action: TableActionsEnum.EDIT,
      label: 'Editar',
      icon: 'pi pi-pencil',
      severity: 'secondary',
      onClick: (row: ProductDto) => {
        this.editProduct(row);
      },
    },
    {
      action: TableActionsEnum.DELETE,
      label: 'Eliminar',
      icon: 'pi pi-trash',
      severity: 'danger',
      onClick: (row: ProductDto) => {
        this.deleteProduct(row);
      },
    },
  ];

  private readonly query = computed(() => ({
    page: this.page(),
    pageSize: this.pageSize(),
    sortBy: this.sortBy() ?? '',
    sortDirection: this.sortDirection(),
    refresh: this.refresh(),
  }));

  private readonly productsPagedInternal = toSignal(
    toObservable(this.query).pipe(
      switchMap((params) => {
        this._loading.set(true);

        return this.productService.getProducts(params).pipe(
          map((result) => {
            if (result.isSuccess() && result.getData()) {
              return result.getData() as PagedList<ProductDto>;
            }
            return this.emptyPaged(params.page, params.pageSize);
          }),
          catchError(() => of(this.emptyPaged(params.page, params.pageSize))),
          tap(() => this._loading.set(false)),
        );
      }),
    ),
    {
      initialValue: this.emptyPaged(1, 10),
    },
  );

  readonly productsPaged = computed(() => this.productsPagedInternal());
  readonly loading = computed(() => this._loading());

  editProduct(row: ProductDto): void {
    this.router.navigate(['/catalog/products/edit', row.id]);
  }

  deleteProduct(row: ProductDto): void {
    this.productService.deleteProduct(row.id).subscribe((result) => {
      if (result.isSuccess()) {
        this.refreshData();
      } else {
        console.error('Error al eliminar el producto:', result.message);
      }
    });
  }

  refreshData(): void {
    this.refresh.set(this.refresh() + 1);
  }

  private emptyPaged(page: number, pageSize: number): PagedList<ProductDto> {
    return {
      items: [],
      totalCount: 0,
      page,
      pageSize,
      totalPages: 0,
      hasNextPage: false,
      hasPreviousPage: false,
    };
  }

  onPageChange(event: PageChangeEvent): void {
    this.page.set(event.page);
    this.pageSize.set(event.pageSize);
  }

  onSortChange(event: SortChangeEvent): void {
    this.sortBy.set(event.sortBy);
    this.sortDirection.set(event.sortDirection);
    this.page.set(1); // reset página
  }

  onRowClick(row: ProductDto): void {
    console.log('Row clicked', row);
  }
}
