import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AppPrimeNGModule } from '../app.prime-ng.module';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';

@NgModule({
  declarations: [NavbarComponent, BreadcrumbComponent],
  imports: [CommonModule, AppPrimeNGModule],
  exports: [NavbarComponent, BreadcrumbComponent],
})
export class SharedModule {}
