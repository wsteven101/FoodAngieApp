import {Bag} from './bag'

export class BagItemNode {
  constructor(
    public quantity: number,
    public bag: Bag
  ) { }
}
