import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ArticleService } from 'src/app/services/article-service';
import { FormBuilder, Validators } from '@angular/forms';
import { Topic } from 'src/app/data-model/models/topic.model';
import { ArticleCreate } from 'src/app/data-model/dto/article-create.dto';
import { LoginService } from 'src/app/services/login-service';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-create-article',
  templateUrl: './create-article.component.html',
  styleUrls: ['./create-article.component.css'],
  providers: [ ArticleService, LoginService ]
})
export class CreateArticleComponent implements OnInit {
  articleCreateModel = this.formBuilder.group({
    title: ['',[Validators.maxLength(200), Validators.required]],
    topic: ['',[Validators.required]],
    overview: ['', [Validators.maxLength(1000), Validators.required]],
    content: ['',[Validators.required]],
    tags: [[],[]],
  });

  topics: Topic[];

  constructor(private router: Router,
              private articleService: ArticleService,
              private loginService: LoginService,
              private dialog: MatDialog,
              private formBuilder: FormBuilder) { }


  ngOnInit() {
    this.articleService.getTopics()
    .subscribe(topics => { this.topics = topics; })
  }

  onTagsUpdated(list: string[]) {
    this.tags.setValue(list);
  }

  onSubmit() {
    let createArticle: ArticleCreate = {
      ...this.articleCreateModel.value
    }

    createArticle.topicId = parseInt(this.topic.value);
    createArticle.userId = this.loginService.getUserId();

    this.articleService.createArticle(createArticle)
    .subscribe(
      result => { this.router.navigate(['article', result.id]); },
      response => {
        this.dialog.open(AlertDialogComponent, {
          width: '40%',
          data: { title: 'Fail', content: response.error.error }
        });
      });
  }

  get title() {
    return this.articleCreateModel.get('title');
  }

  get topic() {
    return this.articleCreateModel.get('topic');
  }

  get overview() {
    return this.articleCreateModel.get('overview');
  }

  get content() {
    return this.articleCreateModel.get('content');
  }

  get tags() {
    return this.articleCreateModel.get('tags');
  }

}
