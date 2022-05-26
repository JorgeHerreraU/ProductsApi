import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductCreateComponent } from './components/product-create/product-create.component';
import { ProductIndexComponent } from './components/product-index/product-index.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { ProductDeleteComponent } from './components/product-delete/product-delete.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { AppPrimeNGModule } from '../app.prime-ng.module';
import { ProductsRoutingModule } from './products-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MessageToastService } from '../shared/services/message-toast.service';

@NgModule({
  declarations: [
    ProductCreateComponent,
    ProductIndexComponent,
    ProductEditComponent,
    ProductDeleteComponent,
    ProductDetailsComponent,
  ],
  imports: [
    CommonModule,
    AppPrimeNGModule,
    ProductsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [MessageToastService],
})
export class ProductsModule {}
