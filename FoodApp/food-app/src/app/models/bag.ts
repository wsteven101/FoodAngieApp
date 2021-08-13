import { BagItemNode } from './BagItemNode';
import { Food } from './food';
import { FoodItemNode } from './FoodItemNode';
import { Nutrition } from './Nutrition'

export class Bag {
  constructor(
    public id: number,
    public name: string,
    public updateData: boolean = false,
    public nutrition: Nutrition = new Nutrition(0,0,0,0,0),
    public foods: FoodItemNode[] = new Array<FoodItemNode>(),
    public bags: BagItemNode[] = new Array<BagItemNode>()) {

  }
}
