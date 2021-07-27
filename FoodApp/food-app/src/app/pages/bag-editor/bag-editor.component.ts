import { Component, OnInit } from '@angular/core';
import { BagService } from '../../services/bag-service';
import { Observable, of } from "rxjs";
import { Bag } from '../../models/bag';

@Component({
  selector: 'app-bag-editor',
  templateUrl: './bag-editor.component.html',
  styleUrls: ['./bag-editor.component.css']
})
export class BagEditorComponent implements OnInit {

  public bags$: Observable<Array<Bag>> = new Observable<Array<Bag>>();

  constructor(bagService: BagService) {

    this.bags$ = bagService
      .getBags("1");

     // subscribe just to pick-up error msgs
    this.bags$.subscribe(
        (data) => { },
        (err) => { console.log(err) }
    );
  }

  ngOnInit(): void {

  }

}
