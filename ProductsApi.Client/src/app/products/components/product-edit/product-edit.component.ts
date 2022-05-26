import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CategoriesService } from '../../../categories/services/categories.service';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { Subcategory } from '../../../subcategories/models/subcategory.model';
import { SubcategoriesService } from '../../../subcategories/services/subcategories.service';
import { Product } from '../../models/product.model';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css'],
})
export class ProductEditComponent {
  public categories$ = this.categoriesService.getAll();

  public form = this.fb.group({
    name: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'change',
      },
    ],
    price: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'change',
      },
    ],
    category: [''],
    subcategories: [null],
  });

  public subcategories$ = this.subcategoriesService.getAll();

  public get name() {
    return this.form.get('name');
  }

  public get price() {
    return this.form.get('price');
  }

  public get category() {
    return this.form.get('category');
  }

  public get subcategories() {
    return this.form.get('subcategories');
  }

  public constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private subcategoriesService: SubcategoriesService,
    private messageService: MessageToastService,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {
    this.productsService
      .getById(this.route.snapshot.params['id'])
      .subscribe((product) => {
        this.form.patchValue(product);
      });
  }

  public onSubmit(): void {
    if (!this.form.valid) {
      return;
    }
    const product: Product = {
      id: this.route.snapshot.params['id'],
      category: this.category?.value.id,
      name: this.name?.value,
      price: this.price?.value,
      subcategories: this.subcategories?.value?.map((s: Subcategory) => s.id),
    };

    this.productsService.update(product).subscribe(() => {
      this.messageService.show({
        key: 'toast',
        severity: 'success',
        summary: 'Product updated',
        detail: 'Product was successfully updated',
      });
    });
  }
}
