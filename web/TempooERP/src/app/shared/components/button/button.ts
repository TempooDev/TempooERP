import { Component, input, output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { NgClass } from '@angular/common';

type ButtonVariant = 'primary' | 'secondary' | 'text' | 'outline';
type ButtonColor = 'primary' | 'success' | 'warning' | 'danger' | 'neutral';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [ButtonModule, NgClass],
  templateUrl: './button.html',
  styleUrls: ['./button.css'],
})
export class ButtonComponent {
  label = input<string>('');
  icon = input<string | undefined>();
  iconPos = input<'left' | 'right' | 'top' | 'bottom'>('left');
  disabled = input<boolean>(false);
  loading = input<boolean>(false);
  variant = input<ButtonVariant>('primary');
  color = input<ButtonColor>('primary');

  clickHandler = output<void>();

  get classes(): string[] {
    const classes: string[] = [];

    switch (this.color()) {
      case 'primary': classes.push('btn-primary'); break;
      case 'success': classes.push('btn-success'); break;
      case 'warning': classes.push('btn-warning'); break;
      case 'danger': classes.push('btn-danger'); break;
      case 'neutral': classes.push('btn-neutral'); break;
    }

    switch (this.variant()) {
      case 'secondary':
        classes.push('btn-secondary');
        break;
      case 'text':
        classes.push('btn-text', 'p-button-text');
        break;
      case 'outline':
        classes.push('btn-outline', 'p-button-outlined');
        break;
      case 'primary':
      default:
        break;
    }

    if (this.loading()) {
      classes.push('is-loading');
    }

    return classes;
  }

  handleClick() {
    if (!this.disabled() && !this.loading()) {
      this.clickHandler.emit();
    }
  }
}
