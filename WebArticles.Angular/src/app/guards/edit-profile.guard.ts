import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { LoginService } from "../services/login-service";


@Injectable({
    providedIn: 'root'
})
export class EditProfileGuard implements CanActivate {
    constructor(private loginService: LoginService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let id: number = parseInt(route.params['id']);
        if (this.loginService.isLoggedIn()) {
            if (id === this.loginService.getUserId()){
                return true;
            }
            else {
                this.router.navigate(['main']);
                return false;
            }
        }

        this.router.navigate(['login'], { queryParams: { returnUrl: state.url }});
        return false;
    }

}
