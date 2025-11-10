import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsService } from '../../../../core/api/catalog/products.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { ProductDto } from '../../../../core/api/catalog/produtc.dto';
import { Result } from '../../../../core/api/result';
import { ProductTableComponent } from "../../components/product-table/product-table.component";
import { emptyPagedList, PagedList } from '../../../../core/api/PagedList';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [CommonModule, ProductTableComponent],
  templateUrl: './products-list.page.html',
})
export class ProductsListPage {
}
