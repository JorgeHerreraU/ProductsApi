import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable, of, switchMap, throwError } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { environment } from '../../../environments/environment';

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
  private readonly baseUrl = environment.apiEndpoint;

  private readonly resource = 'auth';

  public constructor(private authService: AuthService) {}

  private handleError(error: any): Observable<any> {
    this.authService.logout();
    return throwError(() => error);
  }

  public intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return next.handle(request).pipe(
      catchError((error: any) => {
        // Unauthorized
        if (error.status === 401) {
          // If the error comes from a refresh token failed request the user will be logued out [after 1st attempt]
          if (error.url === `${this.baseUrl + this.resource}/refresh`) {
            return this.handleError(error);
          }
          // If there is an unauthorized error we will request a refresh token to the backend [1st attempt]
          if (user.token && user.refreshToken) {
            return this.authService.refresh().pipe(
              switchMap(() => next.handle(request)),
              catchError((error) => this.handleError(error))
            );
          }
        }
        return throwError(() => error);
      })
    );
  }
}
