import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private readonly baseUrl = environment.apiEndpoint;

  private readonly resource = 'users';

  public constructor(private http: HttpClient) {}

  public create(user: User) {
    return this.http.post<User>(`${this.baseUrl + this.resource}`, user);
  }

  public delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl + this.resource}/${id}`);
  }

  public getAll() {
    return this.http.get<User[]>(`${this.baseUrl + this.resource}`);
  }

  public getById(id: number) {
    return this.http.get<User>(`${this.baseUrl + this.resource}/${id}`);
  }

  public update(user: User) {
    return this.http.put<User>(
      `${this.baseUrl + this.resource}/${user.id}`,
      user
    );
  }
}
