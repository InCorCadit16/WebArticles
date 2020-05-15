import { DataSource } from "@angular/cdk/table";
import { BehaviorSubject, Observable, of } from "rxjs";
import { finalize } from "rxjs/operators";
import { CollectionViewer} from "@angular/cdk/collections";
import { UserRow } from "../models/user-row.model";
import { UserService } from "src/app/services/user-service";
import { PaginatorQuery } from "../dto/paginator-query.dto";

export class UsersDataSource implements DataSource<UserRow> {

    private userRows = new BehaviorSubject<UserRow[]>([]);
    total = new BehaviorSubject<number>(0);
    private loadingUsers = new BehaviorSubject<boolean>(false);

    private loading$ = this.loadingUsers.asObservable();

    constructor(private userService: UserService) {}

    connect(collectionViewer: CollectionViewer): Observable<UserRow[] | readonly UserRow[]> {
        return this.userRows.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.userRows.complete();
        this.total.complete();
        this.loadingUsers.complete();
    }

    loadUserRows(query: PaginatorQuery) {
        this.loadingUsers.next(true);
        this.userService.getUserRowsPage(query)
        .pipe(
            finalize(() => this.loadingUsers.next(false))
        )
        .subscribe(data => {
            this.userRows.next(data.items);
            this.total.next(data.total);
        });
    }
}
