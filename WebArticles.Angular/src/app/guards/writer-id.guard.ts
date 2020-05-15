import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { LoginService } from "../services/login-service";
import { UserService } from "../services/user-service";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";


@Injectable({
    providedIn: 'root'
})
export class WriterIdGuard implements CanActivate {
    constructor(private userService: UserService ,private loginService: LoginService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        let articleId = parseInt(route.params['id']);
        if (this.loginService.isLoggedIn()) {
            return this.userService.getUserIdByArticleId(articleId)
            .pipe(
                map(id => id === this.loginService.getUserId())
            )
        }

        this.router.navigate(['login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
