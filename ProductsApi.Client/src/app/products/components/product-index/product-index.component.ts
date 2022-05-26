import { Component, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { Product } from '../../models/product.model';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-product-index',
  templateUrl: './product-index.component.html',
  styleUrls: ['./product-index.component.css'],
})
export class ProductIndexComponent {
  public products$ = this.productsService.getAll();

  public selectedProducts: Product[];

  @ViewChild('dt') public table: Table;

  public constructor(private productsService: ProductsService) {}

  public applyFilterGlobal($event: any, matchMode: string) {
    this.table.filterGlobal(
      ($event.target as HTMLInputElement).value,
      matchMode
    );
  }
}
