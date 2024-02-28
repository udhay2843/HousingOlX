
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { catchError, concatMap, retry, retryWhen } from 'rxjs/operators';
import { AlertifyService } from './alertify.service';
import { Observable, of, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { errorcode } from 'src/app/enums/errorcode';

@Injectable({
  providedIn:'root'
})

export  class HttpErrorInterceptorService implements HttpInterceptor {
  constructor(private alertify:AlertifyService){}
  intercept(req: HttpRequest<any>, next: HttpHandler){
    console.log("HTTP Request Started");
    return next.handle(req)
    .pipe(
      retryWhen(error=>this.retryrequest(error,10)
       ),
      catchError((error:HttpErrorResponse)=>{
        const errorMessage=this.SetError(error);
        console.log(error);
        this.alertify.error(errorMessage);
        return throwError(errorMessage);
      })
    );
  }
  retryrequest(error:Observable<unknown>,retrycount:number):Observable<unknown>{
   return error.pipe(
      concatMap((checkerr:HttpErrorResponse,count:number)=>{
        if(count<=retrycount){
          switch(checkerr.status){
            case errorcode.serverdown :
              return of(checkerr);
              case errorcode.unauthorised :
                return of(checkerr);
          }
        }
     return throwError(checkerr);
  }
      )
      )
  }
  SetError(error:HttpErrorResponse){
    let errorMessage="Some Unknown Error Occured";
    if(error.error instanceof ErrorEvent){
      //client side error
      errorMessage=error.error.message;
    }
    else{
      //server side error
      if(error.status===401){
        return error.statusText;
      }
      if(error.error.errorMessage &&error.status!==0){
      errorMessage=error.error.errorMessage;
    }
    if(!error.error.errorMessage && error.error &&error.status!==0){
      errorMessage=error.error;
    }
    }
    return errorMessage;
    //return error.error;
  }
}
