import { Component } from '@angular/core';
import { BreadcrumbService } from '../../services/breadcrumb.service';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css'],
})
export class BreadcrumbComponent {
  public breadcrumb$ = this.breadcrumbService.breadcrumb$;

  public constructor(private breadcrumbService: BreadcrumbService) {}
}
