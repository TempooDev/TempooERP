import {
  Component,
  ContentChild,
  TemplateRef,
  input,
  output,
} from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';

import { NgTemplateOutlet } from '@angular/common';
import { PagedList } from '../../../core/api/PagedList';
import { TableActions } from '../../enums/table-actions';

export interface TableColumn {
  field: string;
  header: string;
  width?: string;
  sortable?: boolean;
  align?: 'left' | 'center' | 'right';
}

export interface PageChangeEvent {
  page: number; // 1-based
  pageSize: number;
}

export interface SortChangeEvent {
  sortBy: string;
  sortDirection: 'asc' | 'desc';
}

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [TableModule, ButtonModule, NgTemplateOutlet],
  templateUrl: './table.html',
  styleUrls: ['./table.css'],
})
export class TableComponent<T = any> {
  columns = input<TableColumn[]>([]);

  paged = input<PagedList<T> | null>(null);

  loading = input(false);

  rowsPerPageOptions = input<number[]>([10, 20, 50]);

  emptyMessage = input('No hay datos disponibles');

  actions = input<TableActions[]>([]);

  rowClick = output<T>();
  pageChange = output<PageChangeEvent>();
  sortChange = output<SortChangeEvent>();

  defaultActionIcon = 'pi pi-question';

  // body custom opcional
  @ContentChild('body', { static: false }) bodyTemplate?: TemplateRef<any>;

  // Helpers derivados del PagedResult
  get items(): T[] {
    return this.paged()?.items ?? [];
  }

  get pageSize(): number {
    const paged = this.paged();
    return paged?.pageSize ?? this.rowsPerPageOptions()[0] ?? 10;
  }

  get totalRecords(): number {
    return this.paged()?.totalCount ?? 0;
  }

  getFirst(): number {
    const paged = this.paged();
    if (!paged) return 0;
    return (paged.page - 1) * paged.pageSize;
  }

  onRowClick(row: T) {
    this.rowClick.emit(row);
  }

  hasCustomBody(): boolean {
    return !!this.bodyTemplate;
  }

  onPage(event: any): void {
    const rows = event.rows ?? this.pageSize;
    const page = rows > 0 ? event.first / rows + 1 : 1;
    this.pageChange.emit({ page, pageSize: rows });
  }

  onSort(event: any): void {
    if (!event.field) return;

    const sortDirection: 'asc' | 'desc' = event.order === 1 ? 'asc' : 'desc';

    this.sortChange.emit({
      sortBy: event.field,
      sortDirection,
    });
  }
}
