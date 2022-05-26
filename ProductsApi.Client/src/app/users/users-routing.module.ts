import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthGuard } from '../shared/guards/auth.guard';
import { UserCreateComponent } from './components/user-create/user-create.component';
import { UserDeleteComponent } from './components/user-delete/user-delete.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { UserIndexComponent } from './components/user-index/user-index.component';

const routes = [
  {
    path: '',
    component: UserIndexComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'create',
    component: UserCreateComponent,
    data: { breadcrumb: 'Create' },
    canActivate: [AuthGuard],
  },
  {
    path: 'details/:id',
    component: UserDetailsComponent,
    data: { breadcrumb: 'Details' },
    canActivate: [AuthGuard],
  },
  {
    path: 'edit/:id',
    component: UserEditComponent,
    data: { breadcrumb: 'Edit' },
    canActivate: [AuthGuard],
  },
  {
    path: 'delete/:id',
    component: UserDeleteComponent,
    data: { breadcrumb: 'Delete' },
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {}
