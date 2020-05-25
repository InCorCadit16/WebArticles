import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/data-model/models/article/article';
import { ArticleService } from 'src/app/services/article-service';
import { FormBuilder, Validators } from '@angular/forms';
import { Topic } from 'src/app/data-model/models/topic/topic';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { TopicService } from 'src/app/services/topic-service';
import { AsyncSubject, Subject } from 'rxjs';

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.css'],
  providers: [ ArticleService ]
})

export class EditArticleComponent implements OnInit {
  articleEditModel;

  private editorSubject: Subject<any> = new AsyncSubject();

  articleId: number;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private articleService: ArticleService,
              private dialog: MatDialog,
              private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.activatedRoute.params
    .subscribe(params => {
      this.articleService.getArticleById(params.id)
      .subscribe(article => {
          this.articleEditModel = this.formBuilder.group({
            title: [article.title,[Validators.maxLength(200), Validators.required]],
            content: [article.content,[Validators.required]],
            overview: [article.overview, [Validators.required, Validators.maxLength(1000)]],
            tags: [article.tags,[]],
          });

          this.articleId = article.id;
      });
    })
  }

  onTagsUpdated(list: string[]) {
    this.tags.setValue(list);
  }

  onSubmit() {
    let updateArticle: Article = new Article();

    updateArticle.id = this.articleId;
    updateArticle.title = this.title.value;
    updateArticle.content = this.content.value;
    updateArticle.overview = this.overview.value;
    updateArticle.tags = this.tags.value;
    updateArticle.lastEditDate = new Date();

    this.articleService.updateArticle(updateArticle)
    .subscribe(
      result => { this.router.navigate(['article', this.articleId]); },
      response => {
      this.dialog.open(AlertDialogComponent, {
        width: '40%',
        data: { title: 'Fail', content: response.error.error }
      });
    });
  }

  handleEditorInit(event) {
    this.editorSubject.next(event.editor);
    this.editorSubject.complete();
  }

  get title() {
    return this.articleEditModel.get('title');
  }

  get overview() {
    return this.articleEditModel.get('overview');
  }

  get content() {
    return this.articleEditModel.get('content');
  }

  get tags() {
    return this.articleEditModel.get('tags');
  }

}
