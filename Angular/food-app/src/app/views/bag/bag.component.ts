import { Component, OnInit } from '@angular/core';
import { BagService } from '../../services/bag-service';
import { Bag } from '../../models/bag';
import { Nutrition } from '../../models/Nutrition';
import { Observable, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { UserService } from '../../services/user-service';
import { NameIdPair } from '../../models/NameIdPair';
import { MatSelectChange } from '@angular/material/select';
import { BagItemNode } from '../../models/BagItemNode';
import { Food } from '../../models/food';
import { FoodItemNode } from '../../models/FoodItemNode';

@Component({
  selector: 'app-bag',
  templateUrl: './bag.component.html',
  styleUrls: ['./bag.component.css']
})
export class BagComponent implements OnInit {

  public userFoods$: Observable<Array<NameIdPair>> = new Observable<Array<NameIdPair>>();
  public userBags$: Observable<Array<NameIdPair>> = new Observable<Array<NameIdPair>>();
  public bag$: Observable<Bag> = new Observable<Bag>();
  private currentBag: Bag = new Bag(0, "");
  public bagFormGroup: FormGroup;
  public newBag: NameIdPair = new NameIdPair(0,"");
  public emptyBagArray: Array<BagItemNode> = new Array<BagItemNode>();
  public bagList$: Observable<Array<BagItemNode>> = new Observable<Array<BagItemNode>>();
  public foodList$: Observable<Array<FoodItemNode>> = new Observable<Array<FoodItemNode>>();

  constructor(
    private bagService: BagService,
    private userService: UserService,
    private route: ActivatedRoute,
    private fb: FormBuilder) {

    this.bagFormGroup = this.fb.group({
      name: "",
      fat: 0,
      satFat: 0,
      sugar: 0,
      salt: 0
    });

  }

  public loadData(bagName: string) {
    this.userBags$ = this.userService.getAllUserBagNames("1");
    this.loadUserFoods();
  }

  public loadUserFoods() {
    this.userFoods$ = this.userService.getAllUserFoodNames("1");
  }

  ngOnInit(): void {

    this.bagFormGroup = this.fb.group({
      name: "",
      fat: 0,
      satFat: 0,
      sugar: 0,
      salt: 0
    });

    const bagName = this.route.snapshot.queryParamMap.get('name');
    if (bagName != null) {
//      (this.bag$ = this.bagService.getBag(bagName)).subscribe(
      this.bagService.getBag(bagName).subscribe(bag => this.setBag(bag));

        this.loadData(bagName);
    }
  }

  public onBagSelectionChange(namedId: NameIdPair) {
    //var namedId = <NameIdPair>event.value;
    var newBag = new Bag(namedId.id, namedId.name, true);
    var newBagNode = new BagItemNode(1, newBag);
    this.currentBag.bags.push(newBagNode);

    this.bagService.fillBag(this.currentBag).subscribe(bag => this.setBag(bag));
  }

  public onFoodSelectionChange(namedId: NameIdPair) {
    //var namedId = <NameIdPair>event.value;
    var newFood = new Food(namedId.id, namedId.name, true, new Nutrition());
    var newFoodNode = new FoodItemNode(1, newFood);
    this.currentBag.foods.push(newFoodNode);

    this.bagService.fillBag(this.currentBag).subscribe(bag => this.setBag(bag));
  }

  private setBag(bag: Bag) {

    console.log("bag subscription");
    console.log("bag name: " + bag.name + ", fat: '" + bag.nutrition.fat + "'");
    this.currentBag = bag;

    this.bagFormGroup.patchValue({
      name: bag.name,
      fat: bag.nutrition.fat,
      satFat: bag.nutrition.saturatedFat,
      sugar: bag.nutrition.sugar,
      salt: bag.nutrition.salt
    });

    this.bagList$ = of(bag.bags);
    this.foodList$ = of(bag.foods);
  }

  public isSugarInRange() : boolean {
        var inRange = this.currentBag.nutrition.sugar < 20;
    return inRange;
  }
}
