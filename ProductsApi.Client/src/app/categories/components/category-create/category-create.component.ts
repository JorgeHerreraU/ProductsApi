import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { CategoriesService } from '../../services/categories.service';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css'],
  providers: [MessageService],
})
export class CategoryCreateComponent {
  public form = this.fb.group({
    name: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'blur',
      },
    ],
    description: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'blur',
      },
    ],
    priority: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'blur',
      },
    ],
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

  public constructor(
    private fb: FormBuilder,
    private categoriesService: CategoriesService,
    private messageService: MessageToastService
  ) {}

  public onSubmit() {
    this.categoriesService.create(this.form.value).subscribe(() => {
      this.messageService.show({
        key: 'toast',
        severity: 'success',
        summary: 'Category created',
        detail: 'Category was successfully created',
      });
      this.form.reset();
    });
  }
}
