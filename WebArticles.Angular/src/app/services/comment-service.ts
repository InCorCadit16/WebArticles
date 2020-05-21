import { Injectable } from "@angular/core";

import { Comment } from "../data-model/models/comment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { PaginatorAnswer } from "../data-model/infrastructure/models/paginator-answer";
import { PaginatorQuery } from "../data-model/infrastructure/models/paginator-query";
import { CommentCreate } from "../data-model/models/comment-create";

@Injectable()
export class CommentService {

    constructor(private http: HttpClient) { }

    getCommentsPage(articleId: number, query: PaginatorQuery) {
        return this.http.post<PaginatorAnswer<Comment>>(`api/comments/article/${articleId}`, query);
    }

    getCommentsPageByUserId(userId: number, query: PaginatorQuery) {
        return this.http.post<PaginatorAnswer<Comment>>(`api/comments/user/${userId}`, query);
    }

    updateComment(commentId: number, newContent: string) {
        return this.http.put(`api/comments`, { id: commentId, newContent: newContent });
    }

    deleteComment(commentId: number) {
        return this.http.delete(`api/comments/${commentId}`);
    }

    createComment(createComment: CommentCreate) {
        return this.http.post<number>(`api/comments`, createComment);
    }

    updateCommentRating(commentId: number, newRating: number) {
        return this.http.put<number>('api/comments/rating', { id: commentId, rating: newRating });
    }
}
