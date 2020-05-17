import { Component, OnInit, Input } from '@angular/core';
import { CommentService } from 'src/app/services/comment-service';
import { LoginService } from 'src/app/services/login-service';
import { CommentCreate } from 'src/app/data-model/dto/comment-create.dto';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CommentListComponent } from 'src/app/user/reusable/comment-list/comment-list.component';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css'],
  providers: [ CommentService, LoginService ]
})
export class AddCommentComponent implements OnInit {
  commentText: string = "";

  @Input() articleId: number;
  @Input() commentList: CommentListComponent;

  constructor(private commentService: CommentService,
              private loginService: LoginService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private dialog: MatDialog) { }

  ngOnInit() {
  }

  postComment() {
    if (this.loginService.isLoggedIn()) {
      let commentCreate: CommentCreate = {
        userId: this.loginService.getUserId(),
        articleId: this.articleId,
        content: this.commentText
      }

      this.commentService.createComment(commentCreate)
      .subscribe(
        successResult => {
          this.commentText = "";
          this.commentList.onCommentListUpdated();
          this.dialog.open(AlertDialogComponent, {
            width: '40%',
            data:{ title: 'Success', content: "Your comment was successfuly posted and most probably is in the end of the list" }
          });
      },
      response => {
        this.dialog.open(AlertDialogComponent, {
          width: '40%',
          data: { title: 'Fail', content: response.error.error }
        });
      });
    } else {
      this.activatedRoute.url.subscribe(url => {
        this.router.navigate(['login'], { queryParams: { returnUrl: url.map(s => s.path).join('/') }});
      })
    }

  }

}
