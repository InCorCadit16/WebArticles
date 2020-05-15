import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/data-model/models/article.model';
import { ArticleService } from 'src/app/services/article-service';
import { FormBuilder, Validators } from '@angular/forms';
import { Topic } from 'src/app/data-model/models/topic.model';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { TopicService } from 'src/app/services/topic-service';

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.css'],
  providers: [ ArticleService, TopicService ]
})

export class EditArticleComponent implements OnInit {
  articleEditModel = this.formBuilder.group({
    title: ['',[Validators.maxLength(200), Validators.required]],
    topic: ['',[Validators.required]],
    content: ['',[Validators.required]],
    tags: [[],[]],
  });

  topics: Topic[];
  articleId: number;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private articleService: ArticleService,
              private topicService: TopicService,
              private dialog: MatDialog,
              private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.activatedRoute.params
    .subscribe(params => {
      this.articleService.getArticleById(params.id)
      .subscribe(article => {
          this.articleId = article.id;
          this.title.setValue(article.title);
          this.topic.setValue(article.topic.id);
          this.content.setValue(article.content);
          this.tags.setValue(article.tags);
      });
    })

    this.topicService.getTopics()
    .subscribe(topics => { this.topics = topics; })
  }

  onTagsUpdated(list: string[]) {
    this.tags.setValue(list);
  }

  onSubmit() {
    let updateArticle: Article = new Article();

    updateArticle.id = this.articleId;
    updateArticle.title = this.title.value;
    updateArticle.topic = this.topics.find(t => t.id === parseInt(this.topic.value));
    updateArticle.content = this.content.value;
    updateArticle.tags = this.tags.value;

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

  get title() {
    return this.articleEditModel.get('title');
  }

  get topic() {
    return this.articleEditModel.get('topic');
  }

  get content() {
    return this.articleEditModel.get('content');
  }

  get tags() {
    return this.articleEditModel.get('tags');
  }

}
