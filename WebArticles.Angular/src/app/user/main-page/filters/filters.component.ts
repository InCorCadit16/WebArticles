import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { Observable} from 'rxjs';
import { TopicsFilterComponent } from './topics-filter/topics-filter.component';
import { RatingFilterComponent } from './rating-filter/rating-filter.component';
import { DateFilterComponent } from './date-filter/date-filter.component';
import { TagsFilterComponent } from './tags-filter/tags-filter.component';
import { Filters } from 'src/app/data-model/dto/filters.dto';
import { Topic } from 'src/app/data-model/models/topic.model';
import { TopicService } from 'src/app/services/topic-service';

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

  @Output() applyFilters = new EventEmitter<Filters>();

  constructor(private topicService: TopicService) {

  }

  ngOnInit() {
    this.topics = this.topicService.getTopics();
  }

  onApplyFilters() {
      let chosenTopics = this.topicsFilter.topicsListView.options.map((o) =>  o.selected? o.value: "")
      .filter(val => val.length != 0);

      let filters: Filters = new Filters();

      // Topics
      if (chosenTopics.length != 0)
        filters.topics = JSON.stringify(chosenTopics);

      // Rating
      if (this.ratingFilter.minValue != null)
        filters.minRating = +this.ratingFilter.minValue;

      if (this.ratingFilter.maxValue != null)
        filters.maxRating = +this.ratingFilter.maxValue;

      // Publish Date
      let start = this.dateFilter.startDate;
      let end = this.dateFilter.endDate;


      if (start != null) {
        start.setHours(0,0);
        filters.minDate = start.toJSON();
      }


      if (end != null) {
        end.setHours(23,59);
        filters.maxDate = end.toJSON();
      }

      // Tags
      if (this.tagsFilter.tags.length != 0)
        filters.tags = '#' + this.tagsFilter.tags.join('#');

      this.applyFilters.emit(filters);
  }

}
