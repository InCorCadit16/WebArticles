import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { LoginQuery } from "../data-model/dto/login-query.dto";
import { LoginAnswer } from "../data-model/dto/login-answer.dto";
import { RegisterQuery } from "../data-model/dto/register-query.dto";
import { RegisterAnswer } from "../data-model/dto/register-answer.dto";
import { ExternalSignInQuery } from "../data-model/dto/external-signin-query.dto";



@Injectable()
export class LoginService {
    private UserIdClaim = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
    private UserNameClaim = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
    private RolesClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
    private AuthorizationMethodClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod';

    constructor(private http: HttpClient) { }

    register(userRegister: RegisterQuery) {
        return this.http.post<RegisterAnswer>("api/authentication/register", userRegister);
    }

    login(userLogin: LoginQuery) {
        return this.http.post<LoginAnswer>("api/authentication/login", userLogin);
    }

    loginExternal(query: ExternalSignInQuery) {
        return this.http.post<LoginAnswer>("api/authentication/external", query);
    }

    isLoggedIn(): boolean {
        const token = localStorage.getItem('accessToken');
        return token ? true : false;
    }

    logOut() {
        localStorage.removeItem('accessToken');
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

    isAuthorizedExternal(): boolean {
        if (this.isLoggedIn()) {
            let data = JSON.parse(atob(localStorage.getItem('accessToken').split('.')[1]));
            return data[this.AuthorizationMethodClaim] === 'external';
        }
        return null;
    }

    getToken() {
        return localStorage.getItem('accessToken');
    }


}
