import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserCreateComponent } from './components/user-create/user-create.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { UserIndexComponent } from './components/user-index/user-index.component';
import { UserDeleteComponent } from './components/user-delete/user-delete.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { AppPrimeNGModule } from '../app.prime-ng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageToastService } from '../shared/services/message-toast.service';
import { UsersRoutingModule } from './users-routing.module';

@NgModule({
  declarations: [
    UserCreateComponent,
    UserDetailsComponent,
    UserIndexComponent,
    UserDeleteComponent,
    UserEditComponent,
  ],
  imports: [
    CommonModule,
    AppPrimeNGModule,
    UsersRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [MessageToastService],
})
export class UsersModule {}
