import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { User } from "../data-model/models/user.model";
import { UpdateAnswer } from "../data-model/dto/update-answer.dto";


@Injectable()
export class UserService {

    constructor(private http: HttpClient) { }

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

}
