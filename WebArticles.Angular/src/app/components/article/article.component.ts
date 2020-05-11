import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from 'src/app/services/article-service'
import { Article } from 'src/app/data-model/models/article.model';
import { LoginService } from 'src/app/services/login-service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css'],
  providers: [ ArticleService, LoginService ]
})
export class ArticleComponent implements OnInit {
  article: Article;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private articleService: ArticleService,
              public loginService: LoginService) { }

  ngOnInit() {
    this.activatedRoute.params
    .subscribe(params => {
      this.articleService.getArticleById(params.id)
      .subscribe(article => { this.article = article });
    })
  }

  onEditClicked() {
    this.router.navigate(['article',this.article.id,'edit']);
  }

}
