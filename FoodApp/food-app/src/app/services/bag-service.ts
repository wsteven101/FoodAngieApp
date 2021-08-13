import { Inject, OnInit } from "@angular/core";
import { Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Bag } from '../models/bag';
import { Observable } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable({
  providedIn: 'root',
})

export class BagService {

  constructor(private httpClient: HttpClient) { }

  public getBag(id: string): Observable<Bag> {

    //const options = id ? { params: new HttpParams().set('id', id) } : {};
    const options = id ? { params: new HttpParams().set('id', id) } : {};

    let urlStr: string = 'http://localhost:54657/api/Bag/' + id;
    console.log("Order Repository calling:" + urlStr);

    return this.httpClient.get<Bag>(urlStr, options);
  }

  public getBags(userId: string): Observable<Array<Bag>> {

    //const options = userId ? { params: new HttpParams().set('userId', userId) } : {};
    const options = { params: new HttpParams().set('userId', userId) };

    let urlStr: string = 'http://localhost:54657/' + 'api/bag/userbags/' + userId;
    urlStr = "http://localhost:54657/api/bag/userbags/1";
    console.log("Order Repository calling:" + urlStr);
 
    return this.httpClient.get<Array<Bag>>(urlStr,options);
  }

  public fillBag(bag: Bag): Observable<Bag> {

    //const options = id ? { params: new HttpParams().set('id', id) } : {};

    let urlStr: string = 'http://localhost:54657/api/bag/fillbag/';
    console.log("Order Repository calling:" + urlStr);

    return this.httpClient.put<Bag>(urlStr, bag)
      .pipe(
        e => {
          console.log("fillBag() failed");
          return e;
        });
    
  }

}
