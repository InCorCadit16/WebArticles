import { Component, OnInit, ViewChild } from '@angular/core';
import { EditableTagsListComponent } from 'src/app/components/shared/editable-tags-list/editable-tags-list.component';

@Component({
  selector: 'app-tags-filter',
  templateUrl: './tags-filter.component.html',
  styleUrls: ['./tags-filter.component.css']
})
export class TagsFilterComponent implements OnInit {
  @ViewChild(EditableTagsListComponent, { static: true }) editableTagsList: EditableTagsListComponent;

  constructor() { }

  ngOnInit() {
  }

  get tags() {
    return this.editableTagsList.tags;
  }

}
