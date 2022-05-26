import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Subcategory } from '../models/subcategory.model';

@Injectable({
  providedIn: 'root',
})
export class SubcategoriesService {
  private readonly baseUrl = environment.apiEndpoint;

  private readonly resource = 'subcategories';

  public constructor(private http: HttpClient) {}

  public create(subcategory: any) {
    return this.http.post<Subcategory>(
      `${this.baseUrl + this.resource}`,
      subcategory
    );
  }

  public delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl + this.resource}/${id}`);
  }

  public getAll() {
    return this.http.get<Subcategory[]>(`${this.baseUrl + this.resource}`);
  }

  public getById(id: number) {
    return this.http.get<Subcategory>(`${this.baseUrl + this.resource}/${id}`);
  }

  public update(subcategory: any) {
    return this.http.put<Subcategory>(
      `${this.baseUrl + this.resource}/${subcategory.id}`,
      subcategory
    );
  }
}
