import { Component, Input, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Bag } from '../../models/bag';

@Component({
  selector: 'app-bag-card',
  templateUrl: './bag-card.component.html',
  styleUrls: ['./bag-card.component.css']
})
export class BagCardComponent implements OnInit {

  private _bag: Bag = new Bag(0,"N/A");

  @Input()
  get bag() { return this._bag }
  set bag(bagIn: Bag) {
    this._bag = bagIn;
  }

  constructor() { }

  ngOnInit(): void {
  }

}
