import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user-service';
import { LoginService } from 'src/app/services/login-service';
import { ArticleService } from 'src/app/services/article-service';
import { User } from 'src/app/data-model/models/user.model';
import { MatSelectionList, MatDialog } from '@angular/material';
import { Topic } from 'src/app/data-model/models/topic.model';
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
  // pattern for url and custom validator for date
  userUpdateModel = this.formBuilder.group({
    profilePickLink: ['',[Validators.maxLength(200), CustomValidators.isURL()]],
    firstName: ['',[Validators.minLength(2), Validators.maxLength(30)]],
    lastName: ['',[Validators.minLength(2), Validators.maxLength(30)]],
    email: [{ value:'', disabled: this.loginService.isAuthorizedExternal() },[Validators.required, Validators.maxLength(30), Validators.email]],
    birthDate: ['',[CustomValidators.dateInPast()]],
    writerDescription: ['', [Validators.maxLength(2000)]],
    writerTopics: [[], []],
    reviewerDescription: ['', [Validators.maxLength(2000)]],
    reviewerTopics: [[], []]
  });

  userId: number;
  topics: Topic[];

  constructor(private formBuilder: FormBuilder,
              private userService: UserService,
              private loginService: LoginService,
              private topicSerice: TopicService,
              private dialog: MatDialog,
              private router: Router) { }

  ngOnInit() {
    console.log(this.router.getCurrentNavigation())
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

      this.topicSerice.getTopics()
      .subscribe(topics => {
        this.topics = topics;
        this.writerTopics.setValue(topics.map(t => {
          return {  name: t.topicName,
                    selected: user.writerTopics.filter(top => top.topicName === t.topicName).length === 1 }
        }));

        this.reviewerTopics.setValue(topics.map(t => {
          return {  name: t.topicName,
                    selected: user.reviewerTopics.filter(top => top.topicName === t.topicName).length === 1 }
        }));
      });
  }

  onWriterTopicSelected(list: MatSelectionList) {
    this.writerTopics.setValue(list.options.map(t =>{ return { name: t.value, selected: t.selected }}))
  }

  onReviewerTopicSelected(list: MatSelectionList) {
    this.reviewerTopics.setValue(list.options.map(t =>{ return { name: t.value, selected: t.selected }}))
  }

  onSubmit() {
    let userUpdate: User = {
      ...this.userUpdateModel.value
    }

    userUpdate.id = this.userId;
    userUpdate.email = this.email.value;
    userUpdate.writerTopics = this.writerTopics.value.filter(t => t.selected).map(t => {
       return { topicName: t.name, id: this.topics.filter(s => s.topicName === t.name)[0].id }
      });
    userUpdate.reviewerTopics = this.reviewerTopics.value.filter(t => t.selected).map(t => {
      return { topicName: t.name, id: this.topics.filter(s => s.topicName === t.name)[0].id }
     });

    this.userService.updateUser(userUpdate)
    .subscribe(
    successResult => { this.router.navigate(['profile', this.userId]); },
    response => {
      this.dialog.open(AlertDialogComponent, {
        width: '40%',
        data: { title: 'Fail', content: response.error.error }
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

  get writerTopics() {
    return this.userUpdateModel.get('writerTopics');
  }

  get reviewerDescription() {
    return this.userUpdateModel.get('reviewerDescription');
  }

  get reviewerTopics() {
    return this.userUpdateModel.get('reviewerTopics');
  }

}
