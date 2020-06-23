import { Component, OnInit, Input } from '@angular/core';
import { ArticleService } from 'src/app/services/article-service';
import { CommentService } from 'src/app/services/comment-service';
import { Article } from 'src/app/data-model/models/article/article';
import { LoginService } from 'src/app/services/login-service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: [ './rating.component.css' ],
  providers: [ ArticleService, CommentService, LoginService ]
})
export class RatingComponent implements OnInit {
  @Input() rating: number;
  @Input() changable: boolean;
  @Input() sourceId: number;
  @Input() sourceType: string;

  state = 0;

  constructor(private articleService: ArticleService,
              private commentService: CommentService,
              private loginService: LoginService,
              private router: Router) { }

  ngOnInit() {
    if (this.changable) {
      if (this.sourceType === 'article')
        this.articleService.getUserArticleMark(this.sourceId).subscribe(mark => { this.state = mark });
      else
        this.commentService.getUserCommentMark(this.sourceId).subscribe(mark => { this.state = mark });
    }
  }

  onUpClicked() {
    if (!this.loginService.isLoggedIn())
      this.router.navigate(['login'])

    if (this.state == 1)
      this.changeRating(0);
    else
      this.changeRating(1);
  }

  onDownClicked() {
    if (!this.loginService.isLoggedIn())
      this.router.navigate(['login'])

    if (this.state == -1)
      this.changeRating(0);
    else
      this.changeRating(-1);
  }

  changeRating(changeWith: number) {
    if (this.sourceType === 'article') {
      this.articleService.updateArticleRating(this.sourceId, changeWith)
      .subscribe(newRating => { this.rating = newRating })
    } else {
      this.commentService.updateCommentRating(this.sourceId, changeWith)
      .subscribe(newRating => { this.rating = newRating })
    }
    this.state = changeWith;
  }

}
