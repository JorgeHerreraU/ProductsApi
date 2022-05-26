import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryCreateComponent } from './components/category-create/category-create.component';
import { CategoryDetailsComponent } from './components/category-details/category-details.component';
import { CategoryEditComponent } from './components/category-edit/category-edit.component';
import { CategoryIndexComponent } from './components/category-index/category-index.component';
import { CategoriesRoutingModule } from './categories-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppPrimeNGModule } from '../app.prime-ng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryDeleteComponent } from './components/category-delete/category-delete.component';
import { MessageToastService } from '../shared/services/message-toast.service';

@NgModule({
  declarations: [
    CategoryCreateComponent,
    CategoryDetailsComponent,
    CategoryEditComponent,
    CategoryIndexComponent,
    CategoryDeleteComponent,
  ],
  imports: [
    CommonModule,
    AppPrimeNGModule,
    CategoriesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [MessageToastService],
})
export class CategoriesModule {}
