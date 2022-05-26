import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private readonly baseUrl = environment.apiEndpoint;

  private readonly resource = 'products';

  public constructor(private http: HttpClient) {}

  public create(product: Product) {
    return this.http.post<Product>(`${this.baseUrl + this.resource}`, product);
  }

  public delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl + this.resource}/${id}`);
  }

  public getAll() {
    return this.http.get<Product[]>(`${this.baseUrl + this.resource}`);
  }

  public getById(id: number) {
    return this.http.get<Product>(`${this.baseUrl + this.resource}/${id}`);
  }

  public update(product: Product) {
    return this.http.put<Product>(
      `${this.baseUrl + this.resource}/${product.id}`,
      product
    );
  }
}
