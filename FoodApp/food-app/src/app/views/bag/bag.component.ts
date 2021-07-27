import { Component, OnInit } from '@angular/core';
import { BagService } from '../../services/bag-service';
import { Bag } from '../../models/bag';

@Component({
  selector: 'app-bag',
  templateUrl: './bag.component.html',
  styleUrls: ['./bag.component.css']
})
export class BagComponent implements OnInit {

  private bag: Bag = new Bag(0,"");

  constructor(private bagService: BagService) { }

  public load(bagName: string) {

  }

  ngOnInit(): void {
    this.bagService.getBag("Milky Way").subscribe()
  }

}
