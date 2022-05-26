import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
})
export class UserDetailsComponent {
  public user$ = this.usersService.getById(this.route.snapshot.params['id']);

  public constructor(
    private route: ActivatedRoute,
    private usersService: UsersService
  ) {}
}
