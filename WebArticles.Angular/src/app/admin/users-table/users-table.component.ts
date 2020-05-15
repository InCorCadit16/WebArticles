import { Component, OnInit, AfterContentInit, ViewChild } from '@angular/core';
import { UsersDataSource } from 'src/app/data-model/data-sources/user.data-source';
import { UserService } from 'src/app/services/user-service';
import { PaginatorQuery } from 'src/app/data-model/dto/paginator-query.dto';
import { MatPaginator } from '@angular/material';
import { tap } from 'rxjs/internal/operators/tap';
import { LoginService } from 'src/app/services/login-service';

@Component({
  selector: 'admin-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css'],
  providers: [ UserService, LoginService ]
})
export class UsersTableComponent implements OnInit, AfterContentInit {
  displayedColums = ['userName','email','firstName','lastName','articles','writerRating','reviewes','reviewerRating'];
  usersDataSource: UsersDataSource;

  @ViewChild(MatPaginator, {static:true}) userRowsPaginator: MatPaginator;

  constructor(private userService: UserService, private loginService: LoginService) { }

  ngOnInit() {
    this.usersDataSource = new UsersDataSource(this.userService);
    let query = new PaginatorQuery();
    this.usersDataSource.loadUserRows(query);
  }

  ngAfterContentInit() {
    this.usersDataSource.total.subscribe(() => { len => this.userRowsPaginator.length = len; });
    this.userRowsPaginator.pageSize = 10;
    this.userRowsPaginator.pageSizeOptions = [5, 10, 25];
    this.userRowsPaginator.page.pipe(
      tap(() => { this.loadUserRows() })
    )
  }

  loadUserRows() {
    let query = new PaginatorQuery();
    query.page = this.userRowsPaginator.pageIndex + 1;
    query.pageSize = this.userRowsPaginator.pageSize;
    this.usersDataSource.loadUserRows(query);
  }

}
