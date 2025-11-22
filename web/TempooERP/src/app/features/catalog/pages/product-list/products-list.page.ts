import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductTableComponent } from '../../components/product-table/product-table.component';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [CommonModule, ProductTableComponent],
  templateUrl: './products-list.page.html',
})
export class ProductsListPage {}
