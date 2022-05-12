import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const userInfo = JSON.parse(localStorage.getItem('userInfo'));
        const token = userInfo?.token;
        if (token) {
            request = request.clone({
                setHeaders: {
                    Authorization: token
                }
            });
        }
        return next.handle(request);
    }
}
