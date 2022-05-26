import { NgModule } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { RippleModule } from 'primeng/ripple';
import { MenubarModule } from 'primeng/menubar';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { ListboxModule } from 'primeng/listbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessagesModule } from 'primeng/messages';
import { InputNumberModule } from 'primeng/inputnumber';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@NgModule({
  imports: [
    BreadcrumbModule,
    ButtonModule,
    CardModule,
    ConfirmDialogModule,
    DropdownModule,
    InputTextModule,
    InputNumberModule,
    ListboxModule,
    MenubarModule,
    MessagesModule,
    ProgressSpinnerModule,
    RippleModule,
    PasswordModule,
    TableModule,
    ToastModule,
  ],
  exports: [
    BreadcrumbModule,
    ButtonModule,
    CardModule,
    ConfirmDialogModule,
    DropdownModule,
    InputTextModule,
    InputNumberModule,
    ListboxModule,
    MenubarModule,
    MessagesModule,
    PasswordModule,
    ProgressSpinnerModule,
    RippleModule,
    TableModule,
    ToastModule,
  ],
})
export class AppPrimeNGModule {}
