import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard-home.page.html',
})
export class DashboardHomePage {
  stats = [
    { label: 'Usuarios activos', value: 134 },
    { label: 'Pedidos', value: 47 },
    { label: 'Facturas emitidas', value: 23 },
  ];
}
