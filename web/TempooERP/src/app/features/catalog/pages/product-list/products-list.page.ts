import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsService } from '../../../../core/api/catalog/products.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { ProductDto } from '../../../../core/api/catalog/produtc.dto';
import { Result } from '../../../../core/api/result';
import { ProductTableComponent } from "../../components/product-table/product-table.component";

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [CommonModule, ProductTableComponent],
  templateUrl: './products-list.page.html',
})
export class ProductsListPage {
  productService = inject(ProductsService);
  
  productsResult = toSignal(
    this.productService.getProducts(),
    {
      initialValue: new Result<ProductDto[]>({
        success: true,
        data: [],
      }),
    }
  );

  products = computed<ProductDto[]>(
    () => this.productsResult().data ?? []
  );
}
    