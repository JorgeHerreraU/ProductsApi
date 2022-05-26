import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { MustMatch } from '../../../shared/validators/must-match.validator';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  public form = this.fb.group(
    {
      email: [
        '',
        {
          validators: [Validators.required, Validators.email],
          updateOn: 'blur',
        },
      ],
      firstName: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      lastName: [
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
    },
    {
      validators: [MustMatch('password', 'confirmPassword')],
    }
  );

  public get firstName() {
    return this.form.get('firstName');
  }

  public get lastName() {
    return this.form.get('lastName');
  }

  public get email() {
    return this.form.get('email');
  }

  public get password() {
    return this.form.get('password');
  }

  public get confirmPassword() {
    return this.form.get('confirmPassword');
  }

  public constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private messageService: MessageToastService,
    private router: Router
  ) {}

  public onSubmit(): void {
    if (!this.form.valid) {
      return;
    }
    this.authService.register(this.form.value).subscribe(() => {
      this.messageService.show({
        key: 'toast',
        severity: 'success',
        summary: 'User registered',
        detail: 'User was successfully registered',
      });
      this.router.navigate(['/login']);
    });
  }
}
