import { Routes } from '@angular/router';
import { ShellComponent } from './core/layout/shell.component';
import { DASHBOARD_ROUTES } from './features/dashboard/dashboard.routes';
import { CATALOG_ROUTES } from './features/catalog/catalog.routes';

export const routes: Routes = [
  {
    path: '',
    component: ShellComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard',
      },
      {
        path: 'dashboard',
        children: DASHBOARD_ROUTES,
      },
      {
        path: 'catalog',
        children: CATALOG_ROUTES,
      },  
    ],
  },
];
