import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { Observable} from 'rxjs';
import { TopicsFilterComponent } from './topics-filter/topics-filter.component';
import { RatingFilterComponent } from './rating-filter/rating-filter.component';
import { DateFilterComponent } from './date-filter/date-filter.component';
import { TagsFilterComponent } from './tags-filter/tags-filter.component';
import { Topic } from 'src/app/data-model/models/topic/topic';
import { TopicService } from 'src/app/services/topic-service';
import { Filter } from 'src/app/data-model/infrastructure/models/filter';
import { FiltersCompareActions } from 'src/app/data-model/infrastructure/models/filters-compare-actions';
import { RequestFilters } from 'src/app/data-model/infrastructure/models/request-filters';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css'],
  providers: [ TopicService ]
})

export class FiltersComponent implements OnInit {
  topics: Observable<Topic[]>;

  @ViewChild(TopicsFilterComponent, { static: false }) topicsFilter: TopicsFilterComponent;
  @ViewChild(RatingFilterComponent, { static: false }) ratingFilter: RatingFilterComponent;
  @ViewChild(DateFilterComponent, { static: false }) dateFilter: DateFilterComponent;
  @ViewChild(TagsFilterComponent, { static: false }) tagsFilter: TagsFilterComponent;

  @Output() applyFilters = new EventEmitter<RequestFilters>();

  constructor(private topicService: TopicService) {

  }

  ngOnInit() {
    this.topics = this.topicService.getTopics();
  }

  onApplyFilters() {
      let chosenTopics = JSON.stringify(this.topicsFilter.topicsListView.options.map((o) =>  o.selected? o.value: "").filter(val => val.length != 0)).replace(/[\]\[]/g,'');

      let requestFilters = new RequestFilters();

      // Topics
      if (chosenTopics.length != 0)
        requestFilters.filters.push(new Filter("topic.topicName", chosenTopics, FiltersCompareActions.In));


      // Rating
      if (this.ratingFilter.minValue != null)
        requestFilters.filters.push(new Filter("rating", +this.ratingFilter.minValue, FiltersCompareActions.isGreaterThenOrEqual));

      if (this.ratingFilter.maxValue != null)
        requestFilters.filters.push(new Filter("rating", +this.ratingFilter.maxValue, FiltersCompareActions.isLessThenOrEqual));

      // Publish Date
      let start = this.dateFilter.startDate;
      let end = this.dateFilter.endDate;


      if (start != null) {
        start.setHours(0,0);
        requestFilters.filters.push(new Filter("publishDate",`"${start.toJSON()}"`, FiltersCompareActions.isGreaterThenOrEqual));
      }


      if (end != null) {
        end.setHours(23,59);
        requestFilters.filters.push(new Filter("publishDate", `"${end.toJSON()}"`, FiltersCompareActions.isLessThenOrEqual));
      }

      // Tags
      if (this.tagsFilter.tags.length != 0)
        requestFilters.tags = this.tagsFilter.tags;

      this.applyFilters.emit(requestFilters);
  }

}
