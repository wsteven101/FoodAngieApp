import { NutritionalContent } from "./NutritionalContent";

export class FoodItem {

    constructor(
        public id: number,
        public name: string,
        public nutrition: NutritionalContent
    ) {  }

}