import { Routes } from '@angular/router';
import { ProductsListPage } from './pages/product-list/products-list.page';
import { ProductEditPage } from './pages/product-edit/product-edit.page';

export const CATALOG_ROUTES: Routes = [
  {
    path: 'products',
    component: ProductsListPage,
  },
  {
    path: 'products/create',
    component: ProductEditPage,
  },
  {
    path: 'products/edit/:id',
    component: ProductEditPage,
  },
];
