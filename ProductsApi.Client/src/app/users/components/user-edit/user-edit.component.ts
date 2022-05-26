import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css'],
})
export class UserEditComponent {
  public form = this.fb.group({
    id: [''],
    firstname: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'blur',
      },
    ],
    lastname: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'blur',
      },
    ],
    email: [
      '',
      {
        validators: [Validators.required],
        updateOn: 'blur',
      },
    ],
    role: [''],
  });

  public roles = [
    { label: 'Admin', value: 'admin' },
    { label: 'User', value: 'user' },
  ];

  public get firstname() {
    return this.form.get('firstname');
  }

  public get lastname() {
    return this.form.get('lastname');
  }

  public get email() {
    return this.form.get('email');
  }

  public get role() {
    return this.form.get('role');
  }

  public constructor(
    private usersService: UsersService,
    private messageService: MessageToastService,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {
    this.usersService
      .getById(this.route.snapshot.params['id'])
      .subscribe((user) => {
        this.form.patchValue(user);
      });
  }

  public onSubmit() {
    if (!this.form.valid) {
      return;
    }
    this.usersService.update(this.form.value).subscribe(() => {
      this.messageService.show({
        key: 'toast',
        severity: 'success',
        summary: 'User updated',
        detail: 'User was successfully updated',
      });
    });
  }
}
