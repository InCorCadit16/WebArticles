import { Component, OnInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnDestroy {
  searchQuery: string = "";

  queryModifiedSubject = new BehaviorSubject<string>("");
  queryModifiedSubscription: Subscription;
  @Output() queryModified = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
    this.queryModifiedSubscription = this.queryModifiedSubject
      .pipe(
        debounceTime(700),
        distinctUntilChanged()
      ).subscribe(str => {
        this.queryModified.emit(str);
      })
  }

  ngOnDestroy() {
    this.queryModifiedSubscription.unsubscribe();
  }

  searchModified(newVal: string) {
    this.queryModifiedSubject.next(newVal);
  }

}
