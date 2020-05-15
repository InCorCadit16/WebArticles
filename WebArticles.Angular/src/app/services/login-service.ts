import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { UserLoginQuery } from "../data-model/dto/user-login-query.dto";
import { UserLoginAnswer } from "../data-model/dto/user-login-answer.dto";
import { UserRegisterQuery } from "../data-model/dto/user-register-query.dto";
import { UserRegisterAnswer } from "../data-model/dto/user-register-answer.dto";



@Injectable()
export class LoginService {
    private UserIdClaim = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
    private UserNameClaim = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
    private RolesClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

    constructor(private http: HttpClient) { }

    register(userRegister: UserRegisterQuery) {
        return this.http.post<UserRegisterAnswer>("api/authentication/register", userRegister);
    }

    login(userLogin: UserLoginQuery) {
        return this.http.post<UserLoginAnswer>("api/authentication/login", userLogin);
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

    getToken() {
        return localStorage.getItem('accessToken');
    }


}
