import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsService } from '../../../../core/api/catalog/products.service';
import { AbstractControl, FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';

@Component({
    selector: 'app-products-form',
    standalone: true,
    imports: [MessageModule, CommonModule, ButtonModule, InputTextModule, InputNumberModule, CheckboxModule, FormsModule, ReactiveFormsModule],
    templateUrl: './product-form.component.html',
})
export class ProductFormComponent {
    private productsService = inject(ProductsService);
    private fb = inject(FormBuilder);

    createProductForm = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
        price: [0, [Validators.required, Validators.min(0.01), Validators.max(1_000_000)]],
        taxRate: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
        isActive: [true, [Validators.required]],
    });
    private readonly validationMessages: Record<string, Record<string,
        (error: any, c: AbstractControl) => string>> = {

            name: {
                required: () => 'El nombre es obligatorio',
                minlength: e => `Mínimo ${e.requiredLength} caracteres (actual: ${e.actualLength})`,
                maxlength: e => `Máximo ${e.requiredLength} caracteres`
            },
            price: {
                required: () => 'El precio es obligatorio',
                min: e => `El precio mínimo es ${e.min}`,
                max: e => `El precio máximo es ${e.max}`
            },
            taxRate: {
                required: () => 'El impuesto es obligatorio',
                min: () => 'No puede ser negativo',
                max: () => 'No puede ser mayor a 100'
            },
            isActive: {
                required: () => 'Debe indicar si está activo'
            }
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
            map[key] ? map[key](val, c) : `Error: ${key}`
        );
    }

    isInvalid(controlName: string) {
        return this.showErrors(controlName);
    }

    onSubmit() {
        if (this.createProductForm.invalid) {
            this.createProductForm.markAllAsTouched();
            return;
        }
        console.log(this.createProductForm.value);
    }
}
