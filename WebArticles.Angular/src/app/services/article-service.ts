import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";

import { Injectable } from "@angular/core";
import { PaginatorQuery } from "../data-model/dto/paginator-query.dto";
import { PaginatorAnswer } from "../data-model/dto/paginator-answer.dto";
import { Topic } from "../data-model/models/topic.model";
import { Article } from "../data-model/models/article.model";
import { UpdateAnswer } from "../data-model/dto/update-answer.dto";
import { ArticleCreate } from "../data-model/dto/article-create.dto";
import { CreateAnswer } from "../data-model/dto/create-answer.dto";
import { ArticlePreview } from "../data-model/models/article-preview.model";

@Injectable()
export class ArticleService {

    constructor(private http: HttpClient) { }

    getArticlePreviewes(query: PaginatorQuery) {
        let params = query.filters? new HttpParams({fromObject: query.filters as any}) : new HttpParams();
        params = params.set('search', query.search)
                        .set('page', query.page.toString())
                        .set('sortBy', query.sortBy)
                        .set('sortDirection',query.sortDirection);

        return this.http.get<PaginatorAnswer<ArticlePreview>>("api/articles", { params: params })
    }


    getArticlesByUserId(id: number, page: number) {
        let params = new HttpParams().append("page", page.toString());
        return this.http.get<PaginatorAnswer<ArticlePreview>>(`api/articles/user/${id}`,{params: params});
    }


    getArticleById(id: number) {
        return this.http.get<Article>(`api/articles/${id}`);
    }

    updateArticle(article: Article) {
        return this.http.put<UpdateAnswer>('api/articles', article);
    }

    createArticle(createArticle: ArticleCreate) {
        return this.http.post<CreateAnswer>('api/articles', createArticle);
    }

    updateArticleRating(articleId: number, newRating: number) {
        return this.http.put<number>('api/articles/rating', { id: articleId, rating: newRating });
    }

    deleteArticle(id: number) {
        return this.http.delete<UpdateAnswer>(`api/articles/${id}`);
    }

}
