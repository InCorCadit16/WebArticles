import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user-service';
import { LoginService } from 'src/app/services/login-service';
import { ArticleService } from 'src/app/services/article-service';
import { User } from 'src/app/data-model/models/user';
import { MatSelectionList, MatDialog, MatListOption } from '@angular/material';
import { Topic } from 'src/app/data-model/models/topic';
import { Router } from '@angular/router';
import { CustomValidators } from 'src/app/validators/custom-validators';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { TopicService } from 'src/app/services/topic-service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
  providers: [UserService, LoginService, TopicService]
})
export class EditProfileComponent implements OnInit {
  userUpdateModel = this.formBuilder.group({
    profilePickLink: ['', [Validators.maxLength(200), CustomValidators.isURL()]],
    firstName: ['', [Validators.minLength(2), Validators.maxLength(30)]],
    lastName: ['', [Validators.minLength(2), Validators.maxLength(30)]],
    email: ['', [Validators.required, Validators.maxLength(30), Validators.email]],
    birthDate: ['', [CustomValidators.dateInPast()]],
    writerDescription: ['', [Validators.maxLength(2000)]],
    reviewerDescription: ['', [Validators.maxLength(2000)]],
  });

  userId: number;
  topics: Topic[];
  reviewerTopics: string[];
  writerTopics: string[];

  constructor(private formBuilder: FormBuilder,
    private userService: UserService,
    private loginService: LoginService,
    private topicService: TopicService,
    private dialog: MatDialog,
    private router: Router) { }

  ngOnInit() {
    if (this.router.getCurrentNavigation() && this.router.getCurrentNavigation().extras.state.user) {
      console.log('user retrieved from state');
      this.initForm(this.router.getCurrentNavigation().extras.state.user);
    } else {
      this.userService.getUser(this.loginService.getUserId())
        .subscribe(user => {
          this.initForm(user);
        })
    }
  }



  initForm(user: User) {
    this.userId = user.id;
    this.profilePickLink.setValue(user.profilePickLink);
    this.firstName.setValue(user.firstName);
    this.lastName.setValue(user.lastName);
    this.email.setValue(user.email);
    this.birthDate.setValue(user.birthDate);
    this.writerDescription.setValue(user.writerDescription);
    this.reviewerDescription.setValue(user.reviewerDescription);

    this.topicService.getTopics()
      .subscribe(topics => {
        this.topics = topics;
        this.writerTopics = topics.map(t => {
          return {
            name: t.topicName,
            selected: user.writerTopics.filter(top => top.topicName === t.topicName).length === 1
          }
        }).filter(t => t.selected).map(t => t.name);

        this.reviewerTopics = topics.map(t => {
          return {
            name: t.topicName, selected: user.reviewerTopics.filter(top => top.topicName === t.topicName).length === 1
          }
        }).filter(t => t.selected).map(t => t.name);
      });
  }

  onSubmit() {
    let userUpdate: User = {
      ...this.userUpdateModel.value
    }

    userUpdate.id = this.userId;
    userUpdate.email = this.email.value;
    userUpdate.profilePickLink = this.profilePickLink.value;
    userUpdate.writerTopics = this.writerTopics.map(t => {
      return { topicName: t, id: this.topics.filter(s => s.topicName === t)[0].id }
    });
    userUpdate.reviewerTopics = this.reviewerTopics.map(t => {
      return { topicName: t, id: this.topics.filter(s => s.topicName === t)[0].id }
    });

    this.userService.updateUser(userUpdate)
      .subscribe(
        success => { this.router.navigate(['profile', this.userId]); },
        error => {
          this.dialog.open(AlertDialogComponent, {
            width: '40%',
            data: { title: 'Fail', content: error.error }
          });
        });

  }

  get profilePickLink() {
    return this.userUpdateModel.get('profilePickLink');
  }

  get firstName() {
    return this.userUpdateModel.get('firstName');
  }

  get lastName() {
    return this.userUpdateModel.get('lastName');
  }

  get email() {
    return this.userUpdateModel.get('email');
  }

  get birthDate() {
    return this.userUpdateModel.get('birthDate');
  }

  get writerDescription() {
    return this.userUpdateModel.get('writerDescription');
  }

  get reviewerDescription() {
    return this.userUpdateModel.get('reviewerDescription');
  }

}
