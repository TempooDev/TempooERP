import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsService } from '../../../../core/api/catalog/products.service';
import { ProductTableComponent } from "../../components/product-table/product-table.component";
import { emptyPagedList, PagedList } from '../../../../core/api/PagedList';
import { FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [CommonModule, ProductTableComponent],
  templateUrl: './products-list.page.html',
})
export class ProductsListPage {
  private productsService = inject(ProductsService);
  private fb = inject(FormBuilder);

  createProductForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
    price: [0, [Validators.required, Validators.min(0)]],
    taxRate: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
    isActive: [true, [Validators.required]],
  });

}
