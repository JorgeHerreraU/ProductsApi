import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoriesService } from '../../services/categories.service';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.css'],
})
export class CategoryDetailsComponent {
  public category$ = this.categoriesService.getById(
    this.route.snapshot.params['id']
  );

  public constructor(
    private route: ActivatedRoute,
    private categoriesService: CategoriesService
  ) {}
}
