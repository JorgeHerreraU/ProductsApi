import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { catchError, Observable, of } from 'rxjs';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { MustMatch } from '../../../shared/validators/must-match.validator';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css'],
})
export class UserCreateComponent {
  public form = this.fb.group(
    {
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
      password: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      confirmPassword: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      role: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
    },
    {
      validators: [MustMatch('password', 'confirmPassword')],
    }
  );

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

  public get password() {
    return this.form.get('password');
  }

  public get confirmPassword() {
    return this.form.get('confirmPassword');
  }

  public constructor(
    private fb: FormBuilder,
    private usersService: UsersService,
    private messageService: MessageToastService
  ) {}

  private handleError(err: any): Observable<any> {
    const errorMessage = err.error?.title || err.error || err.statusText;
    this.messageService.show({
      key: 'toast',
      severity: 'error',
      summary: 'Error',
      detail: errorMessage,
    });
    return of(undefined);
  }

  public onSubmit() {
    if (!this.form.valid) {
      return;
    }
    this.usersService
      .create(this.form.value)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe((user) => {
        if (user) {
          this.messageService.show({
            key: 'toast',
            severity: 'success',
            summary: 'Success',
            detail: 'User created successfully',
          });
          this.form.reset();
        }
      });
  }
}
