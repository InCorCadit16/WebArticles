import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user-service';
import { LoginService } from 'src/app/services/login-service';
import { MatDialog} from '@angular/material';
import { Topic } from 'src/app/data-model/models/topic/topic';
import { Router } from '@angular/router';
import { CustomValidators } from 'src/app/validators/custom-validators';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { TopicService } from 'src/app/services/topic-service';
import { User } from 'src/app/data-model/models/user/user';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
  providers: [UserService, LoginService, TopicService]
})
export class EditProfileComponent implements OnInit {
  userUpdateModel;

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
    this.userService.getUser(this.loginService.getUserId())
      .subscribe(user => { this.initForm(user) })
  }

  initForm(user: User) {
    this.userId = user.id;

    this.userUpdateModel = this.formBuilder.group({
      profilePickLink: [{value: user.profilePickLink, disabled: this.loginService.isUserSignedInExternal()}, [Validators.maxLength(200), CustomValidators.isURL()]],
      firstName: [user.firstName, [Validators.minLength(2), Validators.maxLength(30)]],
      lastName: [user.lastName, [Validators.minLength(2), Validators.maxLength(30)]],
      email: [{value: user.email, disabled: this.loginService.isUserSignedInExternal()}, [Validators.required, Validators.maxLength(30), Validators.email]],
      birthDate: [user.birthDate, [CustomValidators.dateInPast()]],
      writerDescription: [user.writerDescription, [Validators.maxLength(2000)]],
      reviewerDescription: [user.reviewerDescription, [Validators.maxLength(2000)]],
    });

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
