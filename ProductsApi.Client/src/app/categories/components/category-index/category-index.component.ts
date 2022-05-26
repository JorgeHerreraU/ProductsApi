import { Component, OnInit, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { Category } from '../../models/category.model';
import { CategoriesService } from '../../services/categories.service';

@Component({
  selector: 'app-category-index',
  templateUrl: './category-index.component.html',
  styleUrls: ['./category-index.component.css'],
})
export class CategoryIndexComponent {
  public categories$ = this.categoriesService.getAll();

  public priorities = [
    { label: 'high', value: 'high' },
    { label: 'medium', value: 'medium' },
    { label: 'low', value: 'low' },
  ];

  public selectedCategories: Category[];

  @ViewChild('dt') public table: Table;

  public constructor(private categoriesService: CategoriesService) {}

  public applyFilterGlobal($event: any, matchMode: string) {
    this.table.filterGlobal(
      ($event.target as HTMLInputElement).value,
      matchMode
    );
  }
}
