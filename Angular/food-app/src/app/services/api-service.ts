import { Inject, OnInit } from "@angular/core";
import { Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable({
    providedIn: 'root',
})

export class ApiService<T> {

    protected baseUriPath: string;
    protected subject: BehaviorSubject<T>;
    constructor(baseUriPath: string, protected http: HttpClient)
    {
        this.baseUriPath = baseUriPath;

        var defaultValue: T;
        this.subject = new BehaviorSubject<T>(defaultValue);
    }

    get value$(): Observable<T> {
        return this.subject.asObservable();
    }

    get value(): T {
        return this.subject.getValue();
    }

    set value(newValue: T)
    {
        this.subject.next(newValue);
    }

    get(uri: string): Observable<T>
    {
        var completeUri = this.baseUriPath +uri;
        var responeObs = this.http.get<T>(completeUri).subscribe(
            (responseData) => {
                if (responseData != undefined)
                {
                    this.subject.next(responseData);
                }
            },
            (err)=>console.log("An error occurred calling ApiService.get(uri)",err)
        );

        return this.subject.asObservable();
    }

    post(uri: string, bodyObject: T = null)
    {
        var completeUri = this.baseUriPath +uri;
        if (bodyObject == undefined)
        {
            bodyObject = this.value;
        }
        var completeUri = this.baseUriPath +uri;   
        var responeObs = this.http.post<T>(completeUri, bodyObject).subscribe(
            (responseData) => {
                if (responseData != undefined)
                {
                    this.subject.next(responseData);
                },
            },
            (err)=>console.log("An error occurred calling ApiService.get(uri)",err)
        );

        return this.subject.asObservable();
    }

    put(uri: string, bodyObject: T = undefined)
    {
        var completeUri = this.baseUriPath +uri;
        if (bodyObject == undefined)
        {
            bodyObject = this.value;
        }
        var completeUri = this.baseUriPath +uri;   
        var responeObs = this.http.put<T>(completeUri, bodyObject).subscribe(
            (responseData) => {
                if (responseData != undefined)
                {
                    this.subject.next(responseData);
                },
            },
            (err)=>console.log("An error occurred calling ApiService.get(uri)",err)
        );

        return this.subject.asObservable();
    }
}
