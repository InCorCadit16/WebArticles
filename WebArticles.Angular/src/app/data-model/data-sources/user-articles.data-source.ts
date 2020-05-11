import { DataSource } from "@angular/cdk/table";
import { ArticlePreview } from "../models/article-preview.model";
import { BehaviorSubject, Observable, of } from "rxjs";
import { finalize } from "rxjs/operators";
import { ArticleService } from "src/app/services/article-service";
import { CollectionViewer} from "@angular/cdk/collections";

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

    loadArticlePreviews(id:number, page: number) {
        this.loadingPreview.next(true);
        this.articlesService.getArticlesByUserId(id, page)
        .pipe(
            finalize(() => this.loadingPreview.next(false))
        )
        .subscribe(data => {
            this.articlePreviews.next(data.items);
            this.total.next(data.total);
        });
    }
}
