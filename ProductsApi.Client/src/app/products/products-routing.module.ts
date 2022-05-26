import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../shared/guards/auth.guard';
import { ProductCreateComponent } from './components/product-create/product-create.component';
import { ProductDeleteComponent } from './components/product-delete/product-delete.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { ProductIndexComponent } from './components/product-index/product-index.component';

const routes: Routes = [
  {
    path: '',
    component: ProductIndexComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'create',
    component: ProductCreateComponent,
    data: { breadcrumb: 'Create' },
    canActivate: [AuthGuard],
  },
  {
    path: 'details/:id',
    component: ProductDetailsComponent,
    data: { breadcrumb: 'Details' },
    canActivate: [AuthGuard],
  },
  {
    path: 'edit/:id',
    component: ProductEditComponent,
    data: { breadcrumb: 'Edit' },
    canActivate: [AuthGuard],
  },
  {
    path: 'delete/:id',
    component: ProductDeleteComponent,
    data: { breadcrumb: 'Delete' },
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
