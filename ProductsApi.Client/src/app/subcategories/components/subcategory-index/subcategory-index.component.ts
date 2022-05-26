import { Component, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { Subcategory } from '../../models/subcategory.model';
import { SubcategoriesService } from '../../services/subcategories.service';

@Component({
  selector: 'app-subcategory-index',
  templateUrl: './subcategory-index.component.html',
  styleUrls: ['./subcategory-index.component.css'],
})
export class SubcategoryIndexComponent {
  public priorities = [
    { label: 'high', value: 'high' },
    { label: 'medium', value: 'medium' },
    { label: 'low', value: 'low' },
  ];

  public selectedSubcategories: Subcategory[];

  public subcategories$ = this.subcategoriesService.getAll();

  @ViewChild('dt') public table: Table;

  public constructor(private subcategoriesService: SubcategoriesService) {}

  public applyFilterGlobal($event: any, matchMode: string) {
    this.table.filterGlobal(
      ($event.target as HTMLInputElement).value,
      matchMode
    );
  }
}
