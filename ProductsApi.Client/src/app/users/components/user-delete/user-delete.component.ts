import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { MessageToastService } from '../../../shared/services/message-toast.service';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user-delete',
  templateUrl: './user-delete.component.html',
  styleUrls: ['./user-delete.component.css'],
  providers: [ConfirmationService],
})
export class UserDeleteComponent {
  @ViewChild('cd') public cd: ConfirmDialog;

  public user$ = this.usersService.getById(this.route.snapshot.params['id']);

  public constructor(
    private route: ActivatedRoute,
    private router: Router,
    private usersService: UsersService,
    private confirmationService: ConfirmationService,
    private messageService: MessageToastService
  ) {}

  public onDelete() {
    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.usersService
          .delete(this.route.snapshot.params['id'])
          .subscribe(() => {
            this.messageService.show({
              key: 'toast',
              severity: 'info',
              summary: 'Confirmed',
              detail: 'Record deleted',
            });
            this.router.navigate(['/users']);
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
