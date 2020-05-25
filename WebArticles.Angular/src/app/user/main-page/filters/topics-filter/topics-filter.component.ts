import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { Observable } from 'rxjs';
import { MatSelectionList } from '@angular/material/list';
import { Topic } from 'src/app/data-model/models/topic/topic';


@Component({
  selector: 'app-topics-filter',
  templateUrl: './topics-filter.component.html',
  styleUrls: ['./topics-filter.component.css'],
})

export class TopicsFilterComponent implements OnInit {
  @Input() topics: Observable<Topic[]>;
  @ViewChild("matTopics",{ static: true }) topicsListView: MatSelectionList;

  constructor() {}

  ngOnInit() {
  }

}
