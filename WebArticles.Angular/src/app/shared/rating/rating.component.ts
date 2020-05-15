import { Component, OnInit, Input } from '@angular/core';
import { ArticleService } from 'src/app/services/article-service';
import { CommentService } from 'src/app/services/comment-service';
import { Article } from 'src/app/data-model/models/article.model';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: [ './rating.component.css' ],
  providers: [ ArticleService, CommentService ]
})
export class RatingComponent implements OnInit {
  @Input() rating: number;
  @Input() changable: boolean;
  @Input() sourceId: number;
  @Input() sourceType: string;

  state = 0;


  constructor(private articleService: ArticleService,
              private commentService: CommentService) { }

  ngOnInit() {
  }

  onUpClicked() {
    if (this.state !== 1) {
      this.changeRating(this.state == 0? 1: 2);
      this.state = 1;
    } else {
      this.changeRating(-1)
      this.state = 0;
    }
  }

  onDownClicked() {
    if (this.state !== -1) {
      this.changeRating(this.state == 0? -1: -2);
      this.state = -1;
    } else {
      this.changeRating(1)
      this.state = 0;
    }
  }

  changeRating(changeWith: number) {
    if (this.sourceType === 'article') {
      this.articleService.updateArticleRating(this.sourceId, this.rating + changeWith)
      .subscribe(newRating => { this.rating = newRating })
    } else {
      this.commentService.updateCommentRating(this.sourceId, this.rating + changeWith)
      .subscribe(newRating => { this.rating = newRating })
    }
  }

}
