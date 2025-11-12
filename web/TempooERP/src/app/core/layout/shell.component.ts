import { Component, inject, OnInit, signal} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../components/navbar/navbar';
import { HeaderComponent } from '../components/header/header';
import { HealthService } from '../api/health/health.service';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent, HeaderComponent],
  templateUrl: './shell.component.html',
})
export class ShellComponent implements OnInit {
  healthService = inject(HealthService);
  isSidebarCollapsed = signal(false);
  title = signal('Panel principal');
  backendOk = signal<boolean | null>(null);

  ngOnInit() {
    this.checkBackendHealth();
  }

  checkBackendHealth() {
    this.healthService.check().subscribe({
      next: (isHealthy) => {
        this.backendOk.set(isHealthy);
      },
      error: () => {
        this.backendOk.set(false);
      }
    });
  }


  toggleSidebar() {
    this.isSidebarCollapsed.update((v) => !v);
  }
}
