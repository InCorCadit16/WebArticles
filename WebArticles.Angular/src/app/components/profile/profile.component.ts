import { Component, OnInit, ViewChild, AfterContentInit, AfterViewInit, ViewChildren, QueryList, OnDestroy, ElementRef, SimpleChanges, OnChanges } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { UserService } from 'src/app/services/user-service';
import { User } from 'src/app/data-model/models/user.model';
import { ArticleService } from 'src/app/services/article-service';
import { UserArticleDataSource } from 'src/app/data-model/data-sources/user-articles.data-source';
import { MatPaginator, MatDivider } from '@angular/material';
import { tap } from 'rxjs/operators';
import { LoginService } from 'src/app/services/login-service';
import { isUndefined } from 'util';

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

        this.userArticlesDataSource.loadArticlePreviews(this.userId, 1);
      });
  }

  onEditClicked() {
    let navExtras: NavigationExtras = { state: this.user };
    this.router.navigate(['profile', this.user.id, 'edit'], navExtras);
  }

  ngAfterContentInit() {
      this.userArticlesDataSource.total.subscribe(len => this.articlesPaginator.length = len);
      this.articlesPaginator.pageSize = 5;
      this.articlesPaginator.page.pipe(
        tap(() => {
          this.userArticlesDataSource.loadArticlePreviews(this.userId, this.articlesPaginator.pageIndex + 1);
          window.scroll(0,this.aboveArticles.nativeElement.getBoundingClientRect().top + window.pageYOffset);
        })
      ).subscribe();
  }

  ngOnDestroy() {

  }

  ngOnChanges(changes: SimpleChanges) {
    if (!isUndefined(this.userArticlesDataSource))
      this.userArticlesDataSource.loadArticlePreviews(this.userId, 1);
  }

}
