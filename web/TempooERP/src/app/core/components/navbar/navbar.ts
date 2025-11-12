import { Component, input, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

type NavChild = {
  label: string;
  route: string;
};

type NavItem = {
  icon?: string;
  label: string;
  route?: string;
  children?: NavChild[];
};

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
})
export class NavbarComponent {
  readonly items: NavItem[] = [
    {
      icon: '🏠',
      label: 'Dashboard',
      route: '/dashboard',
    },
    {
      icon: '📦',
      label: 'Catalog',
      children: [
        { label: 'Products', route: '/catalog/products' },
        { label: 'Create product', route: '/catalog/products/create' },
      ],
    },
  ];
  openCatalog = signal(true);

  collapsed = input<boolean>(false);
  backendOk = input<boolean | null>(null);

  toggleCatalog(): void {
    this.openCatalog.update((v) => !v);
  }
}
