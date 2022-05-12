import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { of, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SessionRoutes } from '../routes';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerInterceptor implements HttpInterceptor{

  constructor(protected router: Router) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
    req = req.clone();
    return next.handle(req).pipe(catchError(error => this.handleError(error)));
  }

  public handleError(error: HttpErrorResponse): Observable<any>{
    let handled = false;

    if (error.error instanceof ErrorEvent){
      alert('Inténtalo nuevamente');
    }else{
      if (error.status === 404){
        if (this.router.routerState.snapshot.url === `/${SessionRoutes.LOGIN}`){
          alert('Tus credenciales son incorrectas');
        }else{
          this.router.navigate(['/404'], {replaceUrl: true});
        }
        handled = true;
      } else if (error.status === 400){
        alert(error.error);
        handled = true;
      } else if (error.status === 409) {
        alert(error.error);
        handled = true;
      }
      else if (error.status === 500){
        alert(error.error);
        handled = true;
      }
    }

    if (handled){
      return of(error);
    }else{
      return throwError(error);
    }
  }

}
