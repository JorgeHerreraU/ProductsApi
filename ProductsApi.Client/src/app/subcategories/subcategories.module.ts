import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubcategoryIndexComponent } from './components/subcategory-index/subcategory-index.component';
import { AppPrimeNGModule } from '../app.prime-ng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MessageToastService } from '../shared/services/message-toast.service';
import { SubcategoriesRoutingModule } from './subcategories-routing.module';
import { SubcategoryDetailsComponent } from './components/subcategory-details/subcategory-details.component';
import { SubcategoryEditComponent } from './components/subcategory-edit/subcategory-edit.component';
import { SubcategoryDeleteComponent } from './components/subcategory-delete/subcategory-delete.component';
import { SubcategoryCreateComponent } from './components/subcategory-create/subcategory-create.component';

@NgModule({
  declarations: [
    SubcategoryIndexComponent,
    SubcategoryDetailsComponent,
    SubcategoryEditComponent,
    SubcategoryDeleteComponent,
    SubcategoryCreateComponent,
  ],
  imports: [
    CommonModule,
    AppPrimeNGModule,
    SubcategoriesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [MessageToastService],
})
export class SubcategoriesModule {}
