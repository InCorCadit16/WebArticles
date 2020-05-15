import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-rating-filter',
  templateUrl: './rating-filter.component.html',
  styleUrls: ['./rating-filter.component.css']
})
export class RatingFilterComponent implements OnInit {
  minValue = null;
  maxValue = null;

  constructor() { }

  ngOnInit() {
  }

  numberInputKeyPressed(event) {
    let code = (event as KeyboardEvent).charCode
    if (code < 48 || code > 57)
      event.preventDefault();

  }

}
