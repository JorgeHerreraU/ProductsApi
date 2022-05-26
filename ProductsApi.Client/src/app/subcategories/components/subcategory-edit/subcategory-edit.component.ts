import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../../../categories/models/category.model';
import { CategoriesService } from '../../../categories/services/categories.service';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { Subcategory } from '../../models/subcategory.model';
import { SubcategoriesService } from '../../services/subcategories.service';

@Component({
  selector: 'app-subcategory-edit',
  templateUrl: './subcategory-edit.component.html',
  styleUrls: ['./subcategory-edit.component.css'],
})
export class SubcategoryEditComponent {
  public categories$ = this.categoriesService.getAll();

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
    categories: [null],
  });

  public priorities = [
    { label: 'Low', value: 'low' },
    { label: 'Medium', value: 'medium' },
    { label: 'High', value: 'high' },
  ];

  public get name() {
    return this.form.get('name');
  }

  public get description() {
    return this.form.get('description');
  }

  public get priority() {
    return this.form.get('priority');
  }

  public get categories() {
    return this.form.get('categories');
  }

  public constructor(
    private categoriesService: CategoriesService,
    private subcategoriesService: SubcategoriesService,
    private messageService: MessageToastService,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {
    this.subcategoriesService
      .getById(this.route.snapshot.params['id'])
      .subscribe((subcategory) => {
        this.form.patchValue(subcategory);
      });
  }

  public onSubmit() {
    const subcategory: Subcategory = {
      ...this.form.value,
      categories: this.form.value.categories.map((s: Category) => s.id),
    };
    this.subcategoriesService.update(subcategory).subscribe(() => {
      this.messageService.show({
        key: 'toast',
        severity: 'success',
        summary: 'Subcategory updated',
        detail: 'Subcategory was successfully updated',
      });
    });
  }
}
