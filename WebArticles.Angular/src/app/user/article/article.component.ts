import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from 'src/app/services/article-service'
import { Article } from 'src/app/data-model/models/article.model';
import { LoginService } from 'src/app/services/login-service';
import { MatDialog } from '@angular/material';
import { DeleteDialogComponent } from 'src/app/shared/delete-dialog/delete-dialog.component';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';

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
              private dialog: MatDialog,
              private articleService: ArticleService,
              public loginService: LoginService) { }

  ngOnInit() {
    this.activatedRoute.params
    .subscribe(params => {
      this.articleService.getArticleById(params.id)
      .subscribe(article => { this.article = article });
    })
  }

  onEdit() {
    this.router.navigate(['article',this.article.id,'edit']);
  }

  onDelete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '40%',
      data: { id: this.article.id, text: "Are you sure you want to delete this article?" }
    });

    dialogRef.afterClosed()
    .subscribe(data => {
      if (data) {
        this.articleService.deleteArticle(data)
        .subscribe(
          result => {
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

}
