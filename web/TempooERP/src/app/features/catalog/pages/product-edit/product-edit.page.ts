import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductFormComponent } from '../../components/product-form/product-form.component';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { ProductsService } from '../../../../core/api/catalog/products.service';
import { ProductDto } from '../../../../core/api/catalog/product.dto';
@Component({
  selector: 'app-products-edit',
  standalone: true,
  imports: [CommonModule, ProductFormComponent, RouterModule],
  templateUrl: './product-edit.page.html',
})
export class ProductEditPage implements OnInit {
  router = inject(Router);
  productService = inject(ProductsService);
  productDto = signal<ProductDto | null>(null);
  isLoading = signal<boolean>(true);

  ngOnInit(): void {
    const url = this.router.url;
    if (url.includes('/products/edit/')) {
      const id = url.split('/').pop();
      console.log(`Editing product with ID: ${id}`);

      this.productService.getProductById(id!).subscribe((result) => {
        if (result.success) {
          this.productDto.set(result.data || null);
        } else {
          console.error('Failed to fetch product:', result.message);
        }
        this.isLoading.set(false);
      });
    } else {
      this.isLoading.set(false);
    }
  }
}
