import { FoodItem } from './FoodItem';
import { FoodItemNode } from './FoodItemNode';
import { BagItemNode } from './BagItemNode';


export class BagItem {
    
    constructor(
        public id: number,
        public name: string,
        public bags: Array<BagItemNode>,
        public foods: Array<FoodItemNode> ) { }

}
