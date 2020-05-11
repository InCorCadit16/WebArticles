import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-date-filter',
  templateUrl: './date-filter.component.html',
  styleUrls: ['./date-filter.component.css']
})
export class DateFilterComponent implements OnInit {
  startDate: Date = null;
  endDate: Date = null;

  constructor() { }

  ngOnInit() {
  }

}
