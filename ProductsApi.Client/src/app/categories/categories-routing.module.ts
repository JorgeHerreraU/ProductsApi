import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../shared/guards/auth.guard';
import { CategoryCreateComponent } from './components/category-create/category-create.component';
import { CategoryDeleteComponent } from './components/category-delete/category-delete.component';
import { CategoryDetailsComponent } from './components/category-details/category-details.component';
import { CategoryEditComponent } from './components/category-edit/category-edit.component';
import { CategoryIndexComponent } from './components/category-index/category-index.component';

const routes: Routes = [
  {
    path: '',
    component: CategoryIndexComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'create',
    component: CategoryCreateComponent,
    data: { breadcrumb: 'Create' },
    canActivate: [AuthGuard],
  },
  {
    path: 'details/:id',
    component: CategoryDetailsComponent,
    data: { breadcrumb: 'Details' },
    canActivate: [AuthGuard],
  },
  {
    path: 'edit/:id',
    component: CategoryEditComponent,
    data: { breadcrumb: 'Edit' },
    canActivate: [AuthGuard],
  },
  {
    path: 'delete/:id',
    component: CategoryDeleteComponent,
    data: { breadcrumb: 'Delete' },
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoriesRoutingModule {}
