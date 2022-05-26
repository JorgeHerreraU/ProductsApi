import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, Message, MessageService } from 'primeng/api';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { CategoriesService } from '../../services/categories.service';

@Component({
  selector: 'app-category-delete',
  templateUrl: './category-delete.component.html',
  styleUrls: ['./category-delete.component.css'],
  providers: [ConfirmationService],
})
export class CategoryDeleteComponent {
  public category$ = this.categoriesService.getById(
    this.route.snapshot.params['id']
  );

  @ViewChild('cd') public cd: ConfirmDialog;

  public constructor(
    private categoriesService: CategoriesService,
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
        this.categoriesService
          .delete(this.route.snapshot.params['id'])
          .subscribe(() => {
            this.messageService.show({
              key: 'toast',
              severity: 'info',
              summary: 'Confirmed',
              detail: 'Record deleted',
            });
            this.router.navigate(['/categories']);
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
