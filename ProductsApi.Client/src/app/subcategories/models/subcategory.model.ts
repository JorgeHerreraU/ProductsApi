import { Category } from '../../categories/models/category.model';

export class Subcategory {
  public categories?: Category[] | number[];

  public description: string;

  public id?: number;

  public name: string;

  public priority: string;
}
