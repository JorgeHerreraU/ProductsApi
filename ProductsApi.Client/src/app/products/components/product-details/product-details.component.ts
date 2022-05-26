import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent {
  public product$ = this.productsService.getById(
    this.route.snapshot.params['id']
  );

  public constructor(
    private productsService: ProductsService,
    private route: ActivatedRoute
  ) {}
}
