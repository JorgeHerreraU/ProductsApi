import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { SubcategoriesService } from '../../services/subcategories.service';

@Component({
  selector: 'app-subcategory-create',
  templateUrl: './subcategory-create.component.html',
  styleUrls: ['./subcategory-create.component.css'],
})
export class SubcategoryCreateComponent {
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
    private subcategoriesService: SubcategoriesService,
    private messageService: MessageToastService
  ) {}

  public onSubmit() {
    this.subcategoriesService.create(this.form.value).subscribe(() => {
      this.messageService.show({
        key: 'toast',
        severity: 'success',
        summary: 'Subategory created',
        detail: 'Subategory was successfully created',
      });
      this.form.reset();
    });
  }
}
