import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { User } from "../data-model/models/user";
import { UserRow } from "../data-model/models/user-row";
import { LoginService } from "./login-service";
import { PaginatorAnswer } from "../data-model/infrastructure/models/paginator-answer";
import { PaginatorQuery } from "../data-model/infrastructure/models/paginator-query";


@Injectable()
export class UserService {

    constructor(private http: HttpClient,
        private loginService: LoginService) { }

    getUser(id: number) {
        return this.http.get<User>(`api/users/${id}`);
    }

    updateUser(userModel: User) {
        return this.http.put('api/users', userModel);
    }

    getUserArticleId(articleId: number) {
        return this.http.get<number>(`api/users/my/article/${articleId}`);
    }

    getProfilePickLink(id: number) {
        return this.http.get<any>(`api/users/${id}/pick`);
    }

    deleteUser(id: number) {
        return this.http.delete(`api/users/${id}`);
    }

    getUserRowsPage(query: PaginatorQuery) {
        return this.http.post<PaginatorAnswer<UserRow>>(`api/users`, query);
    }

}
