import { Component, OnInit, ViewChild, AfterContentInit, AfterViewInit, ViewChildren, QueryList, OnDestroy, ElementRef, SimpleChanges, OnChanges } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { UserService } from 'src/app/services/user-service';
import { User } from 'src/app/data-model/models/user/user';
import { ArticleService } from 'src/app/services/article-service';
import { UserArticleDataSource } from 'src/app/data-model/data-sources/user-articles.data-source';
import { MatPaginator, MatDivider, MatDialog } from '@angular/material';
import { tap } from 'rxjs/operators';
import { LoginService } from 'src/app/services/login-service';
import { isUndefined } from 'util';
import { DeleteDialogComponent } from 'src/app/shared/delete-dialog/delete-dialog.component';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { PaginatorQuery } from 'src/app/data-model/infrastructure/models/paginator-query';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [ UserService, ArticleService, LoginService ]
})
export class ProfileComponent implements OnInit, AfterContentInit, OnDestroy, OnChanges {
  userId: number;
  user: User;
  loginService: LoginService;

  userArticlesDataSource: UserArticleDataSource;

  @ViewChild("aboveArticles", {static:true}) aboveArticles: ElementRef;
  @ViewChild("articlesPaginator", {static:true}) articlesPaginator: MatPaginator;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private dialog: MatDialog,
              private userService: UserService,
              private articleService: ArticleService,
              loginSerivce: LoginService) {
                this.loginService = loginSerivce;
              }

  ngOnInit() {
      this.userArticlesDataSource = new UserArticleDataSource(this.articleService);

      this.activatedRoute.params
      .subscribe(params => {
        this.userId = parseInt(params.id);
        this.userService.getUser(this.userId)
        .subscribe(user => { this.user = user });

        let query = new PaginatorQuery();
        query.pageSize = 5;
        query.page = 1;
        this.userArticlesDataSource.loadArticlePreviews(this.userId, query);
      });
  }

  onEdit() {
    let navExtras: NavigationExtras = { state: this.user };
    this.router.navigate(['profile', this.user.id, 'edit'], navExtras);
  }

  onDelete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '40%',
      data: { id: this.user.id, text: "Are you sure you want to delete your profile? This action is irreversible" }
    });

    dialogRef.afterClosed()
    .subscribe(data => {
      if (data) {
        this.userService.deleteUser(data)
        .subscribe(
          result => {
            this.loginService.logOut();
            this.router.navigate(['main']);
          },
          errorResponse => {
            this.dialog.open(AlertDialogComponent, {
              width: '40%',
              data: { title: 'Fail', content: errorResponse.error.error }
            });
          }
        );
      }
    })
  }

  ngAfterContentInit() {
      this.userArticlesDataSource.total.subscribe(len => this.articlesPaginator.length = len);
      this.articlesPaginator.pageSize = 5;
      this.articlesPaginator.pageSizeOptions = [5, 10, 25];
      this.articlesPaginator.page.pipe(
        tap(() => {
          let query = new PaginatorQuery();
          query.page = this.articlesPaginator.pageIndex + 1;
          query.pageSize = this.articlesPaginator.pageSize;
          this.userArticlesDataSource.loadArticlePreviews(this.userId, query);
          window.scroll(0,this.aboveArticles.nativeElement.getBoundingClientRect().top + window.pageYOffset);
        })
      ).subscribe();
  }

  ngOnDestroy() {

  }

  ngOnChanges(changes: SimpleChanges) {
    if (!isUndefined(this.userArticlesDataSource)) {
      let query = new PaginatorQuery();
      query.page = this.articlesPaginator.pageIndex + 1;
      query.pageSize = this.articlesPaginator.pageSize;
      this.userArticlesDataSource.loadArticlePreviews(this.userId, query);
    }
  }

}
