import { Component, inject, input, signal, effect } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { ProductsService } from '../../../../core/api/catalog/products.service';
import {
  AbstractControl,
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { CreateProductCommand } from '../../../../core/api/catalog/createproduct.dto';
import { MessageService } from 'primeng/api';
import { IResult } from '../../../../core/api/result';
import { ProductDto } from '../../../../core/api/catalog/product.dto';
import { UpdateProductCommand } from '../../../../core/api/catalog/update-product.dto';

type MessageSeverity =
  | 'success'
  | 'info'
  | 'warn'
  | 'error'
  | 'secondary'
  | 'contrast';

@Component({
  selector: 'app-products-form',
  standalone: true,
  imports: [
    MessageModule,
    CommonModule,
    ButtonModule,
    InputTextModule,
    InputNumberModule,
    CheckboxModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './product-form.component.html',
})
export class ProductFormComponent {
  private productsService = inject(ProductsService);
  private fb = inject(FormBuilder);
  private messageService = inject(MessageService);
  private location = inject(Location);

  product = input<ProductDto | null>(null);

  resultSignal = signal<IResult<any> | null>(null);

  messagesSignal = signal<
    { severity: MessageSeverity; summary: string; detail: string }[]
  >([]);

  createProductForm = this.fb.group({
    name: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(100)],
    ],
    price: [
      0,
      [Validators.required, Validators.min(0.01), Validators.max(1_000_000)],
    ],
    taxRate: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
    isActive: [true, [Validators.required]],
  });

  constructor() {
    effect(() => {
      const productData = this.product();
      if (productData) {
        this.createProductForm.patchValue({
          name: productData.name,
          price: productData.price,
          taxRate: productData.taxRate,
          isActive: productData.isActive,
        });
      }
    });
  }

  private readonly validationMessages: Record<
    string,
    Record<string, (error: any, c: AbstractControl) => string>
  > = {
    name: {
      required: () => 'El nombre es obligatorio',
      minlength: (e) =>
        `Mínimo ${e.requiredLength} caracteres (actual: ${e.actualLength})`,
      maxlength: (e) => `Máximo ${e.requiredLength} caracteres`,
    },
    price: {
      required: () => 'El precio es obligatorio',
      min: (e) => `El precio mínimo es ${e.min}`,
      max: (e) => `El precio máximo es ${e.max}`,
    },
    taxRate: {
      required: () => 'El impuesto es obligatorio',
      min: () => 'No puede ser negativo',
      max: () => 'No puede ser mayor a 100',
    },
    isActive: {
      required: () => 'Debe indicar si está activo',
    },
  };

  showErrors(controlName: string): boolean {
    const c = this.createProductForm.get(controlName);
    return !!(c && c.invalid && (c.dirty || c.touched));
  }

  controlErrorMessages(controlName: string): string[] {
    const c = this.createProductForm.get(controlName);
    if (!c || !c.errors) return [];
    const map = this.validationMessages[controlName] || {};
    return Object.entries(c.errors).map(([key, val]) =>
      map[key] ? map[key](val, c) : `Error: ${key}`,
    );
  }

  isInvalid(controlName: string) {
    return this.showErrors(controlName);
  }

  onSubmit() {
    if (this.createProductForm.invalid) {
      this.createProductForm.markAllAsTouched();
      this.messageService.add({
        severity: 'warn',
        summary: 'Formulario incompleto',
        detail: 'Revisa los campos obligatorios.',
      });
      return;
    }
    if (this.product()) {
      this.updateProduct();
    } else {
      this.createProduct();
    }
  }

  updateProduct() {
    let productData = UpdateProductCommand.updateProduct(
      this.product()!.id,
      this.createProductForm.value.name!,
      this.createProductForm.value.price!,
      this.createProductForm.value.taxRate!,
      this.createProductForm.value.isActive!,
    );

    this.productsService.updateProduct(productData).subscribe({
      next: (result) => {
        this.resultSignal.set(result);

        const messages: {
          severity: MessageSeverity;
          summary: string;
          detail: string;
        }[] = [];

        if (result.success) {
          messages.push({
            severity: 'success',
            summary: 'Producto actualizado',
            detail: 'El producto se ha actualizado correctamente.',
          });

          this.location.back();
        } else if (result.errors && Object.keys(result.errors).length > 0) {
          Object.entries(result.errors).forEach(([field, msgs]) => {
            (msgs as string[]).forEach((msg) => {
              messages.push({
                severity: 'error',
                summary: `Error en ${field}`,
                detail: msg,
              });
            });
          });
        } else {
          messages.push({
            severity: 'error',
            summary: 'Error',
            detail: result.message ?? 'No se pudo actualizar el producto.',
          });
        }

        this.messagesSignal.set(messages);
      },
      error: () => {
        this.messagesSignal.set([
          {
            severity: 'error',
            summary: 'Error del servidor',
            detail: 'Ha ocurrido un error al actualizar el producto.',
          },
        ]);
      },
    });
  }

  createProduct() {
    let productData = CreateProductCommand.createProduct(
      this.createProductForm.value.name!,
      this.createProductForm.value.price!,
      this.createProductForm.value.taxRate!,
      this.createProductForm.value.isActive!,
    );

    this.productsService.createProduct(productData).subscribe({
      next: (result) => {
        this.resultSignal.set(result);

        const messages: {
          severity: MessageSeverity;
          summary: string;
          detail: string;
        }[] = [];

        if (result.success) {
          messages.push({
            severity: 'success',
            summary: 'Producto creado',
            detail: 'El producto se ha creado correctamente.',
          });
          this.location.back();
        } else if (result.errors && Object.keys(result.errors).length > 0) {
          Object.entries(result.errors).forEach(([field, msgs]) => {
            (msgs as string[]).forEach((msg) => {
              messages.push({
                severity: 'error',
                summary: `Error en ${field}`,
                detail: msg,
              });
            });
          });
        } else {
          messages.push({
            severity: 'error',
            summary: 'Error',
            detail: result.message ?? 'No se pudo crear el producto.',
          });
        }

        this.messagesSignal.set(messages);
      },
      error: () => {
        this.messagesSignal.set([
          {
            severity: 'error',
            summary: 'Error del servidor',
            detail: 'Ha ocurrido un error al crear el producto.',
          },
        ]);
      },
    });
  }
}
