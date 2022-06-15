import { Food } from './food'

export class FoodItemNode {
  constructor(
    public quantity: number,
    public food: Food
  ) { }
}
