import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { User } from "../data-model/models/user.model";
import { UpdateAnswer } from "../data-model/dto/update-answer.dto";
import { PaginatorQuery } from "../data-model/dto/paginator-query.dto";
import { PaginatorAnswer } from "../data-model/dto/paginator-answer.dto";
import { UserRow } from "../data-model/models/user-row.model";
import { LoginService } from "./login-service";


@Injectable()
export class UserService {

    constructor(private http: HttpClient,
        private loginService: LoginService) { }

    getUser(id: number) {
        return this.http.get<User>(`api/users/${id}`);
    }

    updateUser(userModel: User) {
        return this.http.put<UpdateAnswer>('api/users', userModel);
    }

    getUserIdByArticleId(articleId: number) {
        return this.http.get<number>(`api/users/article/${articleId}`);
    }

    getProfilePickLink(id: number) {
        return this.http.get<any>(`api/users/${id}/pick`);
    }

    deleteUser(id: number) {
        return this.http.delete<UpdateAnswer>(`api/users/${id}`);
    }

    getUserRowsPage(query: PaginatorQuery) {
        let params = new HttpParams({fromObject: query as any});
        return this.http.get<PaginatorAnswer<UserRow>>(`api/users/`, { params: params });
    }

}
