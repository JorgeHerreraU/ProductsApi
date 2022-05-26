import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { SubcategoriesService } from '../../services/subcategories.service';

@Component({
  selector: 'app-subcategory-delete',
  templateUrl: './subcategory-delete.component.html',
  styleUrls: ['./subcategory-delete.component.css'],
  providers: [ConfirmationService],
})
export class SubcategoryDeleteComponent {
  @ViewChild('cd') public cd: ConfirmDialog;

  public subcategory$ = this.subcategoriesService.getById(
    this.route.snapshot.params['id']
  );

  public constructor(
    private subcategoriesService: SubcategoriesService,
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
        this.subcategoriesService
          .delete(this.route.snapshot.params['id'])
          .subscribe(() => {
            this.messageService.show({
              key: 'toast',
              severity: 'info',
              summary: 'Confirmed',
              detail: 'Record deleted',
            });
            this.router.navigate(['/subcategories']);
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
