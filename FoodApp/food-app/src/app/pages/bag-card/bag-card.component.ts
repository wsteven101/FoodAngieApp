import { Component, Input, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Router } from '@angular/router';
import { Bag } from '../../models/bag';
import { Nutrition } from '../../models/Nutrition';

@Component({
  selector: 'app-bag-card',
  templateUrl: './bag-card.component.html',
  styleUrls: ['./bag-card.component.css']
})
export class BagCardComponent implements OnInit {

  private _bag: Bag = new Bag(0, "N/A", new Nutrition(0,0,0,0,0));

  @Input()
  get bag() { return this._bag }
  set bag(bagIn: Bag) {
    this._bag = bagIn;
  }

  constructor(private router: Router,
    private route: ActivatedRoute  ) { }

  public viewButton(bagName: string) {
    this.router.navigate(['../bag'], { relativeTo: this.route, queryParams: { name: bagName } });
  }

  ngOnInit(): void {
  }

}
