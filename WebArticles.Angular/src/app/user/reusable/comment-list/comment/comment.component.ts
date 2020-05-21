import { Component, OnInit, Input, OnDestroy, HostListener, Output, EventEmitter } from '@angular/core';
import { Comment } from '../../../../data-model/models/comment';
import { LoginService } from 'src/app/services/login-service';
import { MatDialog } from '@angular/material/dialog';
import { EditDialogComponent, TYPE_BIG } from '../../../../shared/edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from '../../../../shared/delete-dialog/delete-dialog.component';
import { CommentService } from 'src/app/services/comment-service';
import { AlertDialogComponent } from '../../../../shared/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
  providers: [LoginService, CommentService]
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
      data: { entityName: "Comment", value: this.comment.content, type: TYPE_BIG }
    });

    dialogRef.afterClosed().subscribe(data => {
      if (data) {
        this.commentService.updateComment(this.comment.id, data)
          .subscribe(
            success => { this.comment.content = data; },
            error => { this.showErrorDialog(error.error); }
          );
      }
    })
  }

  onDelete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '40%',
      data: { id: this.comment.id, text: "Are you sure you want to delete this comment?" }
    });

    dialogRef.afterClosed()
      .subscribe(data => {
        if (data) {
          this.commentService.deleteComment(data)
            .subscribe(
              success => { this.deleted.emit(); },
              error => { this.showErrorDialog(error.error); }
            );
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
