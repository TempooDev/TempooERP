import { Component, input, output,  } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.html',
})
export class HeaderComponent {
  title = input<string>('TempooERP');
  collapsed = input<boolean>(false);
  sidebarToggle = output<void>();

  onToggle(): void {
    this.sidebarToggle.emit();
  }
}
