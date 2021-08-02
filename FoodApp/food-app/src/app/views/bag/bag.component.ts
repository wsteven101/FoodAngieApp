import { Component, OnInit } from '@angular/core';
import { BagService } from '../../services/bag-service';
import { Bag } from '../../models/bag';
import { Nutrition } from '../../models/Nutrition';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { UserService } from '../../services/user-service';

@Component({
  selector: 'app-bag',
  templateUrl: './bag.component.html',
  styleUrls: ['./bag.component.css']
})
export class BagComponent implements OnInit {

  public bag$: Observable<Bag> = new Observable<Bag>();
  public bagFormGroup: FormGroup;

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

  public load(bagName: string) {

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
      this.bagService.getBag(bagName).subscribe(

        bag => {
          console.log("bag subscription");
          console.log("bag name: " + bag.name + ", fat: '" + bag.nutrition.fat + "'");
          this.bagFormGroup.patchValue({
            name: bag.name,
            fat: bag.nutrition.fat,
            satFat: bag.nutrition.saturatedFat,
            sugar: bag.nutrition.sugar,
            salt: bag.nutrition.salt
          })
        });
    }

  }

}
