import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppPrimeNGModule } from '../app.prime-ng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageToastService } from '../shared/services/message-toast.service';

@NgModule({
  declarations: [RegisterComponent, LoginComponent],
  imports: [
    CommonModule,
    AppPrimeNGModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  providers: [MessageToastService],
})
export class AuthModule {}
