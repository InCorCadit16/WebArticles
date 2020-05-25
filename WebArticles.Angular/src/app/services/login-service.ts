import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { RegisterAnswer } from "../data-model/models/user/register-answer";
import { LoginAnswer } from "../data-model/models/user/login-answer";
import { LoginQuery } from "../data-model/models/user/login-query";
import { RegisterQuery } from "../data-model/models/user/register-query";

@Injectable()
export class LoginService {
    private UserIdClaim = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
    private UserNameClaim = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
    private AuthenticationMethodClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod';
    private RolesClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

    constructor(private http: HttpClient) { }

    register(userRegister: RegisterQuery) {
        return this.http.post<RegisterAnswer>("api/authentication/register", userRegister);
    }

    login(userLogin: LoginQuery) {
        return this.http.post<LoginAnswer>("api/authentication/login", userLogin);
    }

    googleSignIn(token: string) {
        let headers = new HttpHeaders().set("Authorization", `Bearer ${token}`);
        return this.http.post<LoginAnswer>("api/authentication/google", {} ,{headers: headers});
    }

    logOut() {
        this.http.post("api/authentication/logout", {})
        .subscribe( () => localStorage.removeItem('accessToken') );
    }


    /*
     // Methods for manipulation with token and claims
    */

    isLoggedIn(): boolean {
        const token = localStorage.getItem('accessToken');
        return token ? true : false;
    }

    getUserId(): number {
        if (this.isLoggedIn()) {
            let data = JSON.parse(atob(localStorage.getItem('accessToken').split('.')[1]));
            return parseInt(data[this.UserIdClaim]);
        }
        return null;
    }

    getUserName(): string {
        if (this.isLoggedIn()) {
            let data = JSON.parse(atob(localStorage.getItem('accessToken').split('.')[1]));
            return data[this.UserNameClaim];
        }
        return null;
    }

    isUserSignedInExternal(): boolean {
        if (this.isLoggedIn()) {
            let data = JSON.parse(atob(localStorage.getItem('accessToken').split('.')[1]));
            return data[this.AuthenticationMethodClaim] === 'external';
        }
        return null;
    }

    hasUserRole(): boolean {
        if (this.isLoggedIn()) {
            let data = JSON.parse(atob(localStorage.getItem('accessToken').split('.')[1]));
            return data[this.RolesClaim].indexOf('User') > -1;
        }
        return null;
    }

    hasAdminRole(): boolean {
        if (this.isLoggedIn()) {
            let data = JSON.parse(atob(localStorage.getItem('accessToken').split('.')[1]));
            return data[this.RolesClaim].indexOf('Admin') > -1;
        }
        return null;
    }

    getToken() {
        return localStorage.getItem('accessToken');
    }


}
