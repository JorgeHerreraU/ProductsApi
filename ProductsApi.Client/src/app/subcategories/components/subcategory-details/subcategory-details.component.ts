import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SubcategoriesService } from '../../services/subcategories.service';

@Component({
  selector: 'app-subcategory-details',
  templateUrl: './subcategory-details.component.html',
  styleUrls: ['./subcategory-details.component.css'],
})
export class SubcategoryDetailsComponent {
  public subcategory$ = this.subcategoriesService.getById(
    this.route.snapshot.params['id']
  );

  public constructor(
    private route: ActivatedRoute,
    private subcategoriesService: SubcategoriesService
  ) {}
}
