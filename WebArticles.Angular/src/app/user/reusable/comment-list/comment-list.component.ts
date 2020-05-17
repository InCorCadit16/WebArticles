import { Component, OnInit, ViewChild, Input, AfterContentInit, ElementRef, OnChanges, SimpleChanges } from '@angular/core';
import { CommentService } from 'src/app/services/comment-service';
import { PaginatorQuery } from 'src/app/data-model/dto/paginator-query.dto';
import { tap } from 'rxjs/operators';
import { isUndefined } from 'util';
import { CommentsDataSource } from 'src/app/data-model/data-sources/comments.data-source';
import { MatPaginator, MatSort } from '@angular/material';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css'],
  providers: [ CommentService ]
})
export class CommentListComponent implements OnInit, AfterContentInit, OnChanges {
  dataSource: CommentsDataSource;
  lastSort = { active: 'id', direction: 'asc' };

  @Input() id: number;
  @Input() showArticle: boolean;
  @Input() sourceType: string;
  @Input() showSorting: boolean;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild('aboveComments', {static: true}) aboveComments: ElementRef;

  constructor(private commentService: CommentService) { }

  ngOnChanges(changes: SimpleChanges) {
    if (!isUndefined(this.dataSource)) {
      let query = new PaginatorQuery();
      if (this.sourceType === 'article')
        this.dataSource.loadComments(this.id , query);
      else
        this.dataSource.loadCommentsFromUser(this.id, query);
    }

  }

  ngOnInit() {
    this.dataSource = new CommentsDataSource(this.commentService);
    let query = new PaginatorQuery();
    if (this.sourceType === 'article')
      this.dataSource.loadComments(this.id , query);
    else
      this.dataSource.loadCommentsFromUser(this.id, query);
  }

  ngAfterContentInit() {
    this.dataSource.total.subscribe(len => this.paginator.length = len);
    this.paginator.pageSize = 10;
    this.paginator.page.pipe(
      tap(() => {
        this.loadComments();
        window.scroll(0,this.aboveComments.nativeElement.getBoundingClientRect().top + window.pageYOffset - 40)
      })
    ).subscribe();
  }

  onSortChanged(event: {active: string, direction: string}) {
    if (event.direction.length == 0) {
      event.active = "id";
    }
    this.lastSort = event;
    this.loadComments();
  }

  onCommentListUpdated() {
    this.loadComments();
  }

  loadComments() {
    let query = new PaginatorQuery();
    query.page = this.paginator.pageIndex + 1;
    query.pageSize = this.paginator.pageSize;
    query.sortBy = this.lastSort.active;
    query.sortDirection = this.lastSort.direction;
    if (this.sourceType === 'article')
      this.dataSource.loadComments(this.id, query);
    else
      this.dataSource.loadCommentsFromUser(this.id, query);
  }



}
