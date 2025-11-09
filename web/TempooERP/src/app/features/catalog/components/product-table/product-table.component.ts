import { CommonModule } from "@angular/common";
import { Component, input, signal } from "@angular/core";
import { ProductDto } from "../../../../core/api/catalog/produtc.dto";
import { TableColumn, TableComponent } from "../../../../shared/components/table/table";

@Component({
  selector: 'app-product-table',
  standalone: true,
  imports: [CommonModule, TableComponent],
  templateUrl: './product-table.component.html',
})
export class ProductTableComponent {
    products = input<ProductDto[]>([]);

    columns: TableColumn[] = [
    { field: 'name', header: 'Nombre', sortable: true },
    { field: 'price', header: 'Precio', sortable: true, align: 'right' },
  ];
  loading = signal(false);

  onRowClick(product: ProductDto) {
    console.log('Producto seleccionado:', product);
  }
}