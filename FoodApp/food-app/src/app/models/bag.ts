import { Nutrition } from './Nutrition'

export class Bag {
  constructor(
    public id: number,
    public name: string,
    public nutrition: Nutrition  ) { }
}
