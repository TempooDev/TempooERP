import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HealthService } from './core/api/health/health.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
}
