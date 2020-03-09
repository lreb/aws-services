import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { tap, map, filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class RestService {

  // Base url
  baseurl = 'http://localhost:3000';


  constructor(private http: HttpClient) { }

  public getFullUrl(url: string): string {
        return environment.host + url;
    }

  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  public get<T>(url): Observable<T> {
        return this.http.get<T>(this.getFullUrl(url))
        .pipe(
            map(res => {
            return res;
        }),
        catchError(err => {
            return throwError(this.errorHandl(err));
         })
        );
    }
  // POST
  // CreateBug(data): Observable<T> {
  //   return this.http.post<Bug>(this.baseurl + '/bugtracking/', JSON.stringify(data), this.httpOptions)
  //   .pipe(
  //     retry(1),
  //     catchError(this.errorHandl)
  //   )
  // }  

  // // GET
  // GetIssue(id): Observable<Bug> {
  //   return this.http.get<Bug>(this.baseurl + '/bugtracking/' + id)
  //   .pipe(
  //     retry(1),
  //     catchError(this.errorHandl)
  //   )
  // }

  // // GET
  // GetIssues(): Observable<Bug> {
  //   return this.http.get<Bug>(this.baseurl + '/bugtracking/')
  //   .pipe(
  //     retry(1),
  //     catchError(this.errorHandl)
  //   )
  // }

  // // PUT
  // UpdateBug(id, data): Observable<Bug> {
  //   return this.http.put<Bug>(this.baseurl + '/bugtracking/' + id, JSON.stringify(data), this.httpOptions)
  //   .pipe(
  //     retry(1),
  //     catchError(this.errorHandl)
  //   )
  // }

  // // DELETE
  // DeleteBug(id){
  //   return this.http.delete<Bug>(this.baseurl + '/bugtracking/' + id, this.httpOptions)
  //   .pipe(
  //     retry(1),
  //     catchError(this.errorHandl)
  //   )
  // }

  // Error handling
  errorHandl(error) {
     let errorMessage = '';
     if(error.error instanceof ErrorEvent) {
       // Get client-side error
       errorMessage = error.error.message;
     } else {
       // Get server-side error
       errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
     }
     console.log(errorMessage);
     return throwError(errorMessage);
  }

}