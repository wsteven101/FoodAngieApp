import { Nutrition } from './Nutrition'

export class Food {
  constructor(
    public id: number,
    public name: string,
    public nutrition: Nutrition) { }
}
