import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login-service';
import { UserRegisterQuery } from 'src/app/data-model/dto/user-register-query.dto';
import { Router } from '@angular/router';
import { UserLoginQuery } from 'src/app/data-model/dto/user-login-query.dto';
import { MatDialog } from '@angular/material';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  providers: [ LoginService ]
})
export class RegistrationComponent implements OnInit {
  userRegisterModel = this.formBuilder.group({
    firstName: ['',[Validators.minLength(2), Validators.maxLength(30)]],
    lastName: ['',[Validators.minLength(2), Validators.maxLength(30)]],
    username: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
    email: ['',[Validators.required, Validators.maxLength(30), Validators.email]],
    password: ['',[Validators.required, Validators.minLength(8), Validators.maxLength(30)]]
  });

  constructor(private formBuilder: FormBuilder,
              private loginService: LoginService,
              private dialog: MatDialog,
              private router: Router) { }

  ngOnInit() {
  }

  onSubmit() {
    let userRegister: UserRegisterQuery = {
      ...this.userRegisterModel.value
    }

    if (userRegister.lastName === '')
      userRegister.lastName = null;

    if (userRegister.firstName === '')
      userRegister.firstName = null;

    this.loginService.register(userRegister)
    .subscribe(registerAnswer => {
        let loginQuery = new UserLoginQuery();
        loginQuery.username = this.username.value;
        loginQuery.password = this.password.value;

        this.loginService.login(loginQuery)
        .subscribe(loginAnswer => {
              localStorage.setItem('accessToken', loginAnswer.encodedToken);
              this.router.navigate(['profile',loginAnswer.userId]);
        },
        response => {
          this.dialog.open(AlertDialogComponent, {
            width: '40%',
            data: { title: 'Fail', content: response.error.errorMessage }
          });
        });
    },
    response => {
      this.dialog.open(AlertDialogComponent, {
        width: '40%',
        data:{ title: 'Fail', content: response.error.errorMessage}
      });
    });
  }

  get firstName() {
    return this.userRegisterModel.get('firstName');
  }

  get lastName() {
    return this.userRegisterModel.get('lastName');
  }

  get username() {
    return this.userRegisterModel.get('username');
  }

  get email() {
    return this.userRegisterModel.get('email');
  }

  get password() {
    return this.userRegisterModel.get('password');
  }

}
