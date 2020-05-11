import { Component, OnInit, Input, OnDestroy, HostListener, Output, EventEmitter } from '@angular/core';
import { Comment } from '../../../../data-model/models/comment.model';
import { LoginService } from 'src/app/services/login-service';
import { MatDialog } from '@angular/material/dialog';
import { EditDialogComponent } from '../../edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from '../../delete-dialog/delete-dialog.component';
import { CommentService } from 'src/app/services/comment-service';
import { AlertDialogComponent } from '../../alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
  providers: [ LoginService, CommentService ]
})
export class CommentComponent implements OnInit {
  @Input() comment: Comment;
  @Input() showArticle: boolean;

  @Output() deleted = new EventEmitter();

  constructor(public loginService: LoginService,
              private commentService: CommentService,
              private dialog: MatDialog) { }

  ngOnInit() { }

  onEdit() {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '75%',
      data: this.comment.content
    });

    dialogRef.afterClosed().subscribe(data => {
      if (data) {
        this.commentService.updateComment(this.comment.id, data).subscribe(result => {
          if (result.succeeded) {
            this.comment.content = data;
          } else {
            this.showErrorDialog(result.error);
          }
        });
      }
    })
  }

  onDelete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '40%',
      data: this.comment.id
    });

    dialogRef.afterClosed()
    .subscribe(data => {
      if (data) {
        this.commentService.deleteComment(data)
        .subscribe(result => {
          console.log(result);
          if (result.succeeded) {
            this.deleted.emit();
          } else {
            this.showErrorDialog(result.error);
          }
        });
      }
    })
  }

  showErrorDialog(errorMessage: string) {
    this.dialog.open(AlertDialogComponent, {
      width: '40%',
      data: { title: 'Fail', content: errorMessage }
    });
  }

}
