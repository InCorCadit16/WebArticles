import { Component, OnInit, Input } from '@angular/core';
import { ArticlePreview } from 'src/app/data-model/models/article/article-preview';

@Component({
  selector: 'app-article-preview',
  templateUrl: './article-preview.component.html',
  styleUrls: ['./article-preview.component.css']
})
export class ArticlePreviewComponent implements OnInit {
  @Input() articlePreview: ArticlePreview;

  constructor() {

  }

  ngOnInit() {
  }

  AfterContentInit() {

  }
}
