import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject, Observable, of, pipe } from "rxjs";
import { finalize } from "rxjs/operators";
import { CollectionViewer} from "@angular/cdk/collections";
import { Comment } from '../models/comment.model';
import { CommentService } from '../../services/comment-service';
import { PaginatorQuery } from "../dto/paginator-query.dto";
import { LoginService } from "src/app/services/login-service";

export class CommentsDataSource implements DataSource<Comment> {

    private comments = new BehaviorSubject<Comment[]>([]);
    total = new BehaviorSubject<number>(0);
    private loading = new BehaviorSubject<boolean>(false);

    private loading$ = this.loading.asObservable();

    constructor(private commentService: CommentService) {}

    connect(collectionViewer: CollectionViewer): Observable<Comment[] | readonly Comment[]> {
        return this.comments.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.comments.complete();
        this.total.complete();
        this.loading.complete();
    }

    loadComments(articleId:number, query: PaginatorQuery) {
        this.loading.next(true);
        this.commentService.getCommentsPage(articleId, query)
        .pipe(
            finalize(() => this.loading.next(false))
        )
        .subscribe(data => {
            this.comments.next(data.items);
            this.total.next(data.total);
        });
    }

    loadCommentsFromUser(userId: number, query: PaginatorQuery ) {
        this.loading.next(true);
        this.commentService.getCommentsPageByUserId(userId, query)
        .pipe(
            finalize(() => this.loading.next(false))
        )
        .subscribe(data => {
            this.comments.next(data.items);
            this.total.next(data.total);
        });
    }
}
