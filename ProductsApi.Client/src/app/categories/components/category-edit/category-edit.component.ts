import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { Subcategory } from '../../../subcategories/models/subcategory.model';
import { SubcategoriesService } from '../../../subcategories/services/subcategories.service';
import { Category } from '../../models/category.model';
import { CategoriesService } from '../../services/categories.service';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css'],
})
export class CategoryEditComponent {
  public form = this.fb.group({
    id: [''],
    name: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'change',
      },
    ],
    description: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'change',
      },
    ],
    priority: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'change',
      },
    ],
    subcategories: [null],
  });

  public priorities = [
    { label: 'Low', value: 'low' },
    { label: 'Medium', value: 'medium' },
    { label: 'High', value: 'high' },
  ];

  public subcategories$ = this.subcategoriesService.getAll();

  public get name() {
    return this.form.get('name');
  }

  public get description() {
    return this.form.get('description');
  }

  public get priority() {
    return this.form.get('priority');
  }

  public get subcategories() {
    return this.form.get('subcategories');
  }

  public constructor(
    private categoriesService: CategoriesService,
    private subcategoriesService: SubcategoriesService,
    private messageService: MessageToastService,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {
    this.categoriesService
      .getById(this.route.snapshot.params['id'])
      .subscribe((category) => {
        this.form.patchValue(category);
      });
  }

  public onSubmit() {
    if (!this.form.valid) {
      return;
    }
    const category: Category = {
      ...this.form.value,
      subcategories: this.form.value.subcategories.map(
        (s: Subcategory) => s.id
      ),
    };
    this.categoriesService.update(category).subscribe(() => {
      this.messageService.show({
        key: 'toast',
        severity: 'success',
        summary: 'Category updated',
        detail: 'Category was successfully updated',
      });
    });
  }
}
