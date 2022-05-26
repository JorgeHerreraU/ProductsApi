import { Subcategory } from '../../subcategories/models/subcategory.model';

export class Category {
  public description: string;

  public id?: number;

  public name: string;

  public priority: string;

  public subcategories?: Subcategory[] | number[];
}
