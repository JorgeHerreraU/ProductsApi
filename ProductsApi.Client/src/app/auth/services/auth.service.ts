import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { User } from '../../users/models/user.model';
import { LoginForm } from '../interfaces/login.interface';
import { RegisterForm } from '../interfaces/register.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _user$ = new BehaviorSubject<User>(null!);

  private readonly baseUrl = environment.apiEndpoint;

  private readonly resource = 'auth';

  public get user$() {
    const user = localStorage.getItem('user');
    if (user) {
      this._user$.next(JSON.parse(user));
    }
    return this._user$.asObservable();
  }

  public constructor(private http: HttpClient, private router: Router) {}

  public login(loginForm: LoginForm) {
    return this.http
      .post<User>(`${this.baseUrl + this.resource}/login`, loginForm)
      .pipe(
        tap((user: User) => {
          this._user$.next(user);
          localStorage.setItem('user', JSON.stringify(user));
        })
      );
  }

  public logout() {
    localStorage.removeItem('user');
    this._user$.next(null!);
    this.router.navigate(['/login']);
  }

  public refresh() {
    return this.http.post<User>(`${this.baseUrl + this.resource}/refresh`, {
      token: JSON.parse(localStorage.getItem('user')!).token,
      refreshToken: JSON.parse(localStorage.getItem('user')!).refreshToken,
    });
  }

  public register(registerForm: RegisterForm) {
    return this.http.post<User>(
      `${this.baseUrl + this.resource}/register`,
      registerForm
    );
  }
}
