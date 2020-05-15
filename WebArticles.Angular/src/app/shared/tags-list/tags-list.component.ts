import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tags-list',
  templateUrl: './tags-list.component.html',
  styleUrls: ['./tags-list.component.css']
})
export class TagsListComponent implements OnInit {
  @Input() tags: string[];

  constructor() { }

  ngOnInit() {
  }

}
