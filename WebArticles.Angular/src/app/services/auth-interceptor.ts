import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { LoginService } from './login-service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor  {

    constructor(private loginService: LoginService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.loginService.isLoggedIn())
            req = req.clone({headers : req.headers.append("Authorization", `Bearer ${this.loginService.getToken()}`)});

        return next.handle(req);
    }
}

export const AuthInterceptorProvider = {
    provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true
}
