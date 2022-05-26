import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private readonly baseUrl = environment.apiEndpoint;

  private readonly resource = 'categories';

  public constructor(private http: HttpClient) {}

  public create(category: Category) {
    return this.http.post<Category>(
      `${this.baseUrl + this.resource}`,
      category
    );
  }

  public delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl + this.resource}/${id}`);
  }

  public getAll() {
    return this.http.get<Category[]>(`${this.baseUrl + this.resource}`);
  }

  public getById(id: number) {
    return this.http.get<Category>(`${this.baseUrl + this.resource}/${id}`);
  }

  public update(category: Category) {
    return this.http.put<Category>(
      `${this.baseUrl + this.resource}/${category.id}`,
      category
    );
  }
}
