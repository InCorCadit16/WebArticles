import { Component, OnInit } from '@angular/core';
import { Topic } from 'src/app/data-model/models/topic/topic';
import { TopicService } from 'src/app/services/topic-service';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { EditDialogComponent, TYPE_SMALL } from 'src/app/shared/edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from 'src/app/shared/delete-dialog/delete-dialog.component';

@Component({
  selector: 'admin-topics-menu',
  templateUrl: './topics-menu.component.html',
  styleUrls: ['./topics-menu.component.css'],
  providers: [TopicService]
})
export class TopicsMenuComponent implements OnInit {
  topics: Topic[];
  newTopic = "";


  constructor(private topicService: TopicService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.loadTopics();
  }

  onAdd(newTopic: string) {
    if (newTopic.indexOf(',') > -1) {
      this.dialog.open(AlertDialogComponent, {
        width: '40%',
        data: { title: 'Fail', content: "Unacceptable character ','" }
      });
      return;
    }

    this.newTopic = "";
    let topic = new Topic();
    topic.topicName = newTopic;
    this.topicService.addTopic(topic)
      .subscribe(
        success => { this.topics.push(success) },
        error => {
          this.dialog.open(AlertDialogComponent, {
            width: '40%',
            data: { title: 'Fail', content: error.error }
          });
        }
      );
  }

  onEdit(topic: Topic) {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: "50%",
      data: { entityName: "Topic", value: topic.topicName, type: TYPE_SMALL }
    });

    dialogRef.afterClosed().subscribe(data => {
      if (data) {
        if (data.indexOf(',') > -1) {
          this.dialog.open(AlertDialogComponent, {
            width: '40%',
            data: { title: 'Fail', content: "Unacceptable character ','" }
          });
          return;
        }

        topic.topicName = data;
        this.topicService.updateTopic(topic)
          .subscribe(
            success => {
              let toRep = this.topics.indexOf(this.topics.find(t => t.id === topic.id));
              this.topics.splice(toRep, 1, success);
            },
            error => {
              this.dialog.open(AlertDialogComponent, {
                width: "40%",
                data: { title: "Fail", content: error.error }
              })
            }
          )
      }
    })
  }

  onDelete(topic: number) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: "40%",
      data: { id: topic, text: "Are you sure you want to delete this topic?" }
    });

    dialogRef.afterClosed().subscribe(data => {
      if (data) {
        this.topicService.deleteTopic(data)
          .subscribe(
            () => {
              let toRep = this.topics.indexOf(this.topics.find(t => t.id === topic));
              this.topics.splice(toRep, 1);
            },
            error => {
              this.dialog.open(AlertDialogComponent, {
                width: "40%",
                data: { title: "Fail", content: error.error }
              })
            }
          )
      }
    })
  }

  loadTopics() {
    this.topicService.getTopics()
      .subscribe(
        topics => { this.topics = topics; }
      );
  }

}
