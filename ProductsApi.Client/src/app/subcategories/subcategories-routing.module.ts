import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SubcategoryCreateComponent } from './components/subcategory-create/subcategory-create.component';
import { SubcategoryDeleteComponent } from './components/subcategory-delete/subcategory-delete.component';
import { SubcategoryDetailsComponent } from './components/subcategory-details/subcategory-details.component';
import { SubcategoryEditComponent } from './components/subcategory-edit/subcategory-edit.component';
import { SubcategoryIndexComponent } from './components/subcategory-index/subcategory-index.component';

const routes: Routes = [
  {
    path: '',
    component: SubcategoryIndexComponent,
  },
  {
    path: 'create',
    component: SubcategoryCreateComponent,
    data: { breadcrumb: 'Create' },
  },
  {
    path: 'details/:id',
    component: SubcategoryDetailsComponent,
    data: { breadcrumb: 'Details' },
  },
  {
    path: 'edit/:id',
    component: SubcategoryEditComponent,
    data: { breadcrumb: 'Edit' },
  },
  {
    path: 'delete/:id',
    component: SubcategoryDeleteComponent,
    data: { breadcrumb: 'Delete' },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SubcategoriesRoutingModule {}
