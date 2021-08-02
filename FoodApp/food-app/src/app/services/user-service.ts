import { Inject, OnInit } from "@angular/core";
import { Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Bag } from '../models/bag';
import { Food } from '../models/food';
import { Observable } from "rxjs";
import { map } from 'rxjs/operators';
import { NameIdPair } from "../models/NameIdPair";

@Injectable({
  providedIn: 'root',
})

export class UserService {

  constructor(private httpClient: HttpClient) { }

  public getAllUserFoodNames(userId: string): Observable<NameIdPair[]> {

    return this.getAllUserFoods(userId)
      .pipe(map(foods => {
        var namedIdList = new Array<NameIdPair>();
        foods.forEach
          (food => namedIdList.push(new NameIdPair(food.id, food.name)));
        return namedIdList;
      }));

  }

  public getAllUserFoods(userId: string): Observable<Array<Food>> {

    const options = { params: new HttpParams().set('userId', userId) };

    let urlStr: string = 'http://localhost:54657/' + 'api/user/foods/' + userId;
    urlStr = "http://localhost:54657/api/bag/userbags/1";
    console.log("Order Repository calling:" + urlStr);

    return this.httpClient.get<Array<Food>>(urlStr, options);

  }

}
