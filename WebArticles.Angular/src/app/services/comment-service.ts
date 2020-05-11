import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { PaginatorQuery } from "../data-model/dto/paginator-query.dto";
import { PaginatorAnswer } from "../data-model/dto/paginator-answer.dto";
import { Comment } from "../data-model/models/comment.model";
import { UpdateAnswer } from "../data-model/dto/update-answer.dto";
import { CreateAnswer } from "../data-model/dto/create-answer.dto";
import { CommentCreate } from "../data-model/dto/comment-create.dto";

@Injectable()
export class CommentService {

    constructor(private http: HttpClient) { }

    getCommentsPage(articleId: number, query: PaginatorQuery) {
        let params = new HttpParams({fromObject: query as any});
        return this.http.get<PaginatorAnswer<Comment>>(`api/comments/${articleId}`, { params: params });
    }

    getCommentsPageByUserId(userId: number, query: PaginatorQuery) {
        let params = new HttpParams({fromObject: query as any});
        return this.http.get<PaginatorAnswer<Comment>>(`api/comments/user/${userId}`, { params: params });
    }

    updateComment(commentId: number, newContent: string) {
        return this.http.put<UpdateAnswer>(`api/comments`, { id: commentId, newContent: newContent });
    }

    deleteComment(commentId: number) {
        return this.http.delete<UpdateAnswer>(`api/comments/${commentId}`);
    }

    createComment(createComment: CommentCreate) {
        return this.http.post<CreateAnswer>(`api/comments`, createComment);
    }

    updateCommentRating(commentId: number, newRating: number) {
        return this.http.put<number>('api/comments/rating', { id: commentId, rating: newRating });
    }
}
