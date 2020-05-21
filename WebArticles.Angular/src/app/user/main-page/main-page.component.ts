import { Component, OnInit, ViewChild, AfterContentInit} from '@angular/core';
import { ArticleService } from 'src/app/services/article-service';
import { ArticleDataSource } from 'src/app/data-model/data-sources/article.data-source';
import { SearchComponent } from './search/search.component';
import { MatPaginator, MatSort } from '@angular/material';
import { tap } from 'rxjs/operators';
import { PaginatorQuery } from 'src/app/data-model/infrastructure/models/paginator-query';
import { RequestFilters } from 'src/app/data-model/infrastructure/models/request-filters';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
  providers: [ArticleService]
})

export class MainPageComponent implements OnInit, AfterContentInit {
  dataSource: ArticleDataSource;
  lastFilters: RequestFilters = null;
  lastSort: { active: string, direction: string} = { active:"id", direction:"asc"};
  lastSearch: string = "";

  @ViewChild(SearchComponent, {static: true}) searchBar: SearchComponent;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private articlesService: ArticleService) { }

  onApplyFilters(filters: RequestFilters) {
    this.lastFilters = filters;
    this.loadArticlesPage();
  }

  onSearchModified(newVal: string) {
    this.lastSearch = newVal;
    this.loadArticlesPage();
  }

  onSortChanged(event : { active: string, direction: string }) {
    if (event.direction.length == 0) {
      event.active = "id";
    }
    this.lastSort = event;
    this.loadArticlesPage();
  }

  ngOnInit() {
    this.dataSource = new ArticleDataSource(this.articlesService);
    let query = new PaginatorQuery();
    this.dataSource.loadArticlePreviews(query);
  }

  ngAfterContentInit() {
    this.dataSource.total.subscribe(len => this.paginator.length = len);
    this.paginator.pageSizeOptions = [5, 10, 25]
    this.paginator.pageSize = 10;
    this.paginator.page.pipe(
      tap(() => {this.loadArticlesPage(); window.scroll(0,0)})
    ).subscribe();
  }

  loadArticlesPage() {
    let query = new PaginatorQuery();
    query.filters = this.lastFilters;
    query.searchString = this.lastSearch.toLowerCase();
    query.page = this.paginator.pageIndex + 1;
    query.pageSize = this.paginator.pageSize;
    query.sortBy = this.lastSort.active;
    query.sortDirection = this.lastSort.direction;

    this.dataSource.loadArticlePreviews(query);
  }

}
