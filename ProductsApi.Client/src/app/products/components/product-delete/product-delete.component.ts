import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-product-delete',
  templateUrl: './product-delete.component.html',
  styleUrls: ['./product-delete.component.css'],
  providers: [ConfirmationService],
})
export class ProductDeleteComponent {
  @ViewChild('cd') public cd: ConfirmDialog;

  public product$ = this.productsService.getById(
    this.route.snapshot.params['id']
  );

  public constructor(
    private productsService: ProductsService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageToastService
  ) {}

  public onDelete() {
    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.productsService
          .delete(this.route.snapshot.params['id'])
          .subscribe(() => {
            this.messageService.show({
              key: 'toast',
              severity: 'info',
              summary: 'Confirmed',
              detail: 'Record deleted',
            });
            this.router.navigate(['/products']);
          });
      },
      reject: () => {
        this.messageService.show({
          key: 'toast',
          severity: 'info',
          summary: 'Rejected',
          detail: 'You have rejected',
        });
      },
    });
  }
}
