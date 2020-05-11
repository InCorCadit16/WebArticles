import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-editable-tags-list',
  templateUrl: './editable-tags-list.component.html',
  styleUrls: ['./editable-tags-list.component.css']
})
export class EditableTagsListComponent implements OnInit {

  input: string;

  @Input() tags: string[] = [];
  @Input() title = "Add tags";
  @Output() listUpdated = new EventEmitter<string[]>();

  constructor() { }

  ngOnInit() {
  }

  removeTag(index: number) {
    this.tags.splice(index, 1);
    this.listUpdated.emit(this.tags);
  }

  tagsInputKeyPressed(event: KeyboardEvent) {
    if (event.charCode === 13) {
      let newTags = this.input.split(' ');
      newTags.forEach(element => {
        if (!this.tags.includes(element) && element.length != 0)
              this.tags.unshift(element);
      });
      this.input = "";
      this.listUpdated.emit(this.tags);
      event.preventDefault();
    }
  }


}
