import { Category } from '../../categories/models/category.model';
import { Subcategory } from '../../subcategories/models/subcategory.model';

export class Product {
  public category?: Category | number;

  public id?: number;

  public name: string;

  public price: number;

  public subcategories?: Subcategory[] | number[];
}
