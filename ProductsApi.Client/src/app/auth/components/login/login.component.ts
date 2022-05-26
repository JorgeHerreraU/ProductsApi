import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, first, Observable, of, tap } from 'rxjs';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { User } from '../../../users/models/user.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  public isLoading = false;

  public loginForm = this.fb.group({
    email: [
      '',
      {
        validators: [Validators.required, Validators.email],
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
  });

  public get email() {
    return this.loginForm.get('email');
  }

  public get password() {
    return this.loginForm.get('password');
  }

  public constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private messageService: MessageToastService,
    private router: Router
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

  public onSubmit(): void {
    this.isLoading = true;
    this.authService
      .login(this.loginForm.value)
      .pipe(
        first(),
        catchError((err) => this.handleError(err)),
        tap(() => (this.isLoading = false))
      )
      .subscribe((user: User) => {
        if (user) {
          this.messageService.show({
            key: 'toast',
            severity: 'success',
            summary: 'Success',
            detail: 'You are logged in',
          });
          this.router.navigate(['/']);
        }
      });
  }
}
