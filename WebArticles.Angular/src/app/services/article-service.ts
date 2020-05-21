import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";

import { Injectable } from "@angular/core";
import { ArticleCreate } from "../data-model/models/article-create";
import { ArticlePreview } from "../data-model/models/article-preview";
import { Article } from "../data-model/models/article";
import { PaginatorAnswer } from "../data-model/infrastructure/models/paginator-answer";
import { PaginatorQuery } from "../data-model/infrastructure/models/paginator-query";

@Injectable()
export class ArticleService {

    constructor(private http: HttpClient) { }

    getArticlePreviewes(query: PaginatorQuery) {
        return this.http.post<PaginatorAnswer<ArticlePreview>>("api/articles/paginator", query)
    }


    getArticlesByUserId(id: number, query: PaginatorQuery) {
        return this.http.post<PaginatorAnswer<ArticlePreview>>(`api/articles/user/${id}`, query);
    }


    getArticleById(id: number) {
        return this.http.get<Article>(`api/articles/${id}`);
    }

    updateArticle(article: Article) {
        return this.http.put('api/articles', article);
    }

    createArticle(createArticle: ArticleCreate) {
        return this.http.post<number>('api/articles', createArticle);
    }

    updateArticleRating(articleId: number, newRating: number) {
        return this.http.put<number>('api/articles/rating', { id: articleId, rating: newRating });
    }

    deleteArticle(id: number) {
        return this.http.delete(`api/articles/${id}`);
    }

}
