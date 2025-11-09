import { Routes } from '@angular/router';
import { ProductsListPage } from './pages/product-list/products-list.page';

export const CATALOG_ROUTES: Routes = [
  {
    path: 'products',
    component: ProductsListPage,
  },
];
