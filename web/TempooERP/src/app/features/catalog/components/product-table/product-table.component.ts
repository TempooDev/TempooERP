import { CommonModule } from "@angular/common";
import { Component, computed, inject, input, signal } from "@angular/core";
import { ProductDto } from "../../../../core/api/catalog/produtc.dto";
import { PageChangeEvent, SortChangeEvent, TableColumn, TableComponent } from "../../../../shared/components/table/table";
import { ProductsService } from "../../../../core/api/catalog/products.service";
import { PagedList } from "../../../../core/api/PagedList";
import { toObservable, toSignal } from "@angular/core/rxjs-interop";
import { switchMap } from "rxjs/internal/operators/switchMap";
import { catchError, map, of, tap } from "rxjs";

@Component({
  selector: 'app-product-table',
  standalone: true,
  imports: [CommonModule, TableComponent],
  templateUrl: './product-table.component.html',
})
export class ProductTableComponent {
  private readonly productService = inject(ProductsService);
  private readonly page = signal(1);
  private readonly pageSize = signal(10);
  private readonly sortBy = signal<string | null>(null);
  private readonly sortDirection = signal<'asc' | 'desc'>('asc');

  private readonly _loading = signal(false);

  readonly columns: TableColumn[] = [
    { field: 'name', header: 'Nombre', sortable: true },
    { field: 'price', header: 'Precio', sortable: true, align: 'right' },
    { field: 'isActive', header: 'Activo', sortable: true, align: 'center', width: '90px' },
  ];

  private readonly query = computed(() => ({
    page: this.page(),
    pageSize: this.pageSize(),
    sortBy: this.sortBy() ?? undefined,
    sortDirection: this.sortDirection(),
  }));

  // 👉 llamada al servicio convertida a signal reactivo
  private readonly productsPagedInternal = toSignal(
    toObservable(this.query).pipe(
      switchMap(params => {
        this._loading.set(true);

        return this.productService.getProducts(params).pipe(
          map(result => {
            if (result.success && result.data) {
              return result.data as PagedList<ProductDto>;
            }
            return this.emptyPaged(params.page, params.pageSize);
          }),
          catchError(() =>
            of(this.emptyPaged(params.page, params.pageSize))
          ),
          tap(() => this._loading.set(false))
        );
      })
    ),
    {
      initialValue: this.emptyPaged(1, 10)
    }
  );

  readonly productsPaged = computed(() => this.productsPagedInternal());
  readonly loading = computed(() => this._loading());

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