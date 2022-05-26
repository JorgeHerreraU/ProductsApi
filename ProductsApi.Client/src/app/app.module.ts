import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouteReuseStrategy, RouterModule } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

import { AppComponent } from './app.component';
import { AppRouteReuseStrategy } from './app.routing-strategy';
import { AuthModule } from './auth/auth.module';
import { LoginComponent } from './auth/components/login/login.component';
import { RegisterComponent } from './auth/components/register/register.component';
import { AuthInterceptor } from './shared/interceptors/auth.interceptor';
import { UnauthorizedInterceptor } from './shared/interceptors/unauthorized.interceptor';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { HomeIndexComponent } from './home/components/home-index/home-index.component';

const routes = [
  {
    path: '',
    component: HomeIndexComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { breadcrumb: 'Login' },
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'categories',
    loadChildren: () =>
      import('./categories/categories.module').then((m) => m.CategoriesModule),
    data: { breadcrumb: 'Categories' },
  },
  {
    path: 'subcategories',
    loadChildren: () =>
      import('./subcategories/subcategories.module').then(
        (m) => m.SubcategoriesModule
      ),
    data: { breadcrumb: 'Subcategories' },
  },
  {
    path: 'products',
    loadChildren: () =>
      import('./products/products.module').then((m) => m.ProductsModule),
    data: { breadcrumb: 'Products' },
  },
  {
    path: 'users',
    loadChildren: () =>
      import('./users/users.module').then((m) => m.UsersModule),
    data: { breadcrumb: 'Users' },
  },
];

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    AuthModule,
    SharedModule,
    ToastModule,
    HomeModule,
  ],
  providers: [
    MessageService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UnauthorizedInterceptor,
      multi: true,
    },
    {
      provide: RouteReuseStrategy,
      useClass: AppRouteReuseStrategy,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
