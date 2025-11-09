import { Component, ContentChild, TemplateRef, input, output } from '@angular/core';
import { TableModule } from 'primeng/table';
import { NgTemplateOutlet } from '@angular/common';

export interface TableColumn {
  field: string;
  header: string;
  width?: string;
  sortable?: boolean;
  align?: 'left' | 'center' | 'right';
}

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [TableModule, NgTemplateOutlet],
  templateUrl: './table.html',
  styleUrls: ['./table.css'],
})
export class TableComponent<T = any> {
  columns = input<TableColumn[]>([]);
  value = input<T[]>([]);
  loading = input(false);

  paginator = input(false);
  rows = input(10);
  rowsPerPageOptions = input<number[]>([10, 20, 50]);

  emptyMessage = input('No hay datos disponibles');

  rowClick = output<T>();

  @ContentChild('body', { static: false }) bodyTemplate?: TemplateRef<any>;

  onRowClick(row: T) {
    this.rowClick.emit(row);
  }

  hasCustomBody(): boolean {
    return !!this.bodyTemplate;
  }
}
