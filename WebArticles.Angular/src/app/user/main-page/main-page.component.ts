import { Component, OnInit, ViewChild, AfterContentInit } from '@angular/core';
import { ArticleService } from 'src/app/services/article-service';
import { ArticleDataSource } from 'src/app/data-model/data-sources/article.data-source';
import { SearchComponent } from './search/search.component';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { tap } from 'rxjs/operators';
import { PaginatorQuery } from 'src/app/data-model/infrastructure/models/paginator-query';
import { RequestFilters } from 'src/app/data-model/infrastructure/models/request-filters';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from 'src/app/services/login-service';
import { OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from 'src/app/configs/google-auth.config';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
  providers: [ArticleService, LoginService, OAuthService]
})

export class MainPageComponent implements OnInit, AfterContentInit {
  dataSource: ArticleDataSource;
  lastFilters: RequestFilters = null;
  lastSort: { active: string, direction: string } = { active: "id", direction: "asc" };
  lastSearch: string = "";

  @ViewChild(SearchComponent, { static: true }) searchBar: SearchComponent;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private articlesService: ArticleService,
              private loginService: LoginService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private dialog: MatDialog) {
  }

  onApplyFilters(filters: RequestFilters) {
    this.lastFilters = filters;
    this.loadArticlesPage();
  }

  onSearchModified(newVal: string) {
    this.lastSearch = newVal;
    this.loadArticlesPage();
  }

  onSortChanged(event: { active: string, direction: string }) {
    if (event.direction.length == 0) {
      event.active = "id";
    }
    this.lastSort = event;
    this.loadArticlesPage();
  }

  ngOnInit() {
    this.tryGoogleLogin();

    this.dataSource = new ArticleDataSource(this.articlesService);
    let query = new PaginatorQuery();
    this.dataSource.loadArticlePreviews(query);
  }

  ngAfterContentInit() {
    this.dataSource.total.subscribe(len => this.paginator.length = len);
    this.paginator.pageSizeOptions = [5, 10, 25]
    this.paginator.pageSize = 10;
    this.paginator.page.pipe(
      tap(() => { this.loadArticlesPage(); window.scroll(0, 0) })
    ).subscribe();
  }

  loadArticlesPage() {
    let query = new PaginatorQuery();
    query.filters = this.lastFilters;
    query.searchString = this.lastSearch.toLowerCase();
    query.page = this.paginator.pageIndex + 1;
    query.pageSize = this.paginator.pageSize;
    query.sortBy = this.lastSort.active;
    query.sortDirection = this.lastSort.direction;

    this.dataSource.loadArticlePreviews(query);
  }

  tryGoogleLogin() {
    let params = this.activatedRoute.snapshot.fragment;
    if (params != null) {
      if (params.indexOf("access_token") > -1) {
        params = params.substring(params.indexOf('id_token') + 9);
        let token = params.substring(0, params.indexOf('&'));
        this.loginService.googleSignIn(token)
        .subscribe(
          result => {
            localStorage.setItem("accessToken", result.encodedToken);
            this.router.navigate(["profile", result.userId])
          },
          error => {
            console.log(error);

            this.dialog.open(AlertDialogComponent, {
              width: "40%",
              data: {title: "Fail", content: error.error}
            })
          });
      }
    }
  }

}
