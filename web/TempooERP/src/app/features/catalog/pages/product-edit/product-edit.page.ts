import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ProductFormComponent } from "../../components/product-form/product-form.component";
@Component({
    selector: 'app-products-edit',
    standalone: true,
    imports: [CommonModule, ProductFormComponent],
    templateUrl: './product-edit.page.html',
})
export class ProductEditPage {
}
