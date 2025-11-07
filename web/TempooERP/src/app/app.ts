import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HealthService } from './core/health/health.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TempooERP');
  protected readonly healthService = inject(HealthService);
  backendOk :boolean | null = null;
  ngOnInit(): void {
    this.healthService.check().subscribe(isHealthy => {
      this.backendOk = isHealthy;
      if (isHealthy) {
        console.log('API is healthy');
      } else {
        console.error('API is not reachable');
      }
    });
  }
}
