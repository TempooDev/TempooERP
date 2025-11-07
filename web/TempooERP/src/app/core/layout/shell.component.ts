import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HealthService } from '../api/health/health.service';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './shell.component.html',
})
export class ShellComponent implements OnInit {
  private readonly health = inject(HealthService);

  backendOk: boolean | null = null;

  ngOnInit(): void {
    this.health.check().subscribe(ok => (this.backendOk = ok));
  }
}
