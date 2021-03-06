import { DataSource } from "@angular/cdk/table";
import { ArticlePreview } from "../models/article/article-preview";
import { BehaviorSubject, Observable, of } from "rxjs";
import { finalize } from "rxjs/operators";
import { ArticleService } from "src/app/services/article-service";
import { CollectionViewer} from "@angular/cdk/collections";
import { PaginatorAnswer } from "../infrastructure/models/paginator-answer";
import { PaginatorQuery } from "../infrastructure/models/paginator-query";

export class UserArticleDataSource implements DataSource<ArticlePreview> {

    private articlePreviews = new BehaviorSubject<ArticlePreview[]>([]);
    total = new BehaviorSubject<number>(0);
    private loadingPreview = new BehaviorSubject<boolean>(false);

    private loading$ = this.loadingPreview.asObservable();

    constructor(private articlesService: ArticleService) {}

    connect(collectionViewer: CollectionViewer): Observable<ArticlePreview[] | readonly ArticlePreview[]> {
        return this.articlePreviews.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.articlePreviews.complete();
        this.total.complete();
        this.loadingPreview.complete();
    }

    loadArticlePreviews(id:number, query: PaginatorQuery) {
        this.loadingPreview.next(true);
        this.articlesService.getArticlesByUserId(id, query)
        .pipe(
            finalize(() => this.loadingPreview.next(false))
        )
        .subscribe(data => {
            this.articlePreviews.next(data.items);
            this.total.next(data.total);
        });
    }
}
