import { Component, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user-index',
  templateUrl: './user-index.component.html',
  styleUrls: ['./user-index.component.css'],
})
export class UserIndexComponent {
  public roles = [
    { label: 'admin', value: 'admin' },
    { label: 'user', value: 'user' },
  ];

  @ViewChild('dt') public table: Table;

  public users$ = this.usersService.getAll();

  public constructor(private usersService: UsersService) {}

  public applyFilterGlobal($event: any, matchMode: string) {
    this.table.filterGlobal(
      ($event.target as HTMLInputElement).value,
      matchMode
    );
  }
}
