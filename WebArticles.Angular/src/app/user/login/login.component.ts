import { Component, OnInit, Inject } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { LoginService } from 'src/app/services/login-service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService]
})
export class LoginComponent implements OnInit {
  userLoginModel = this.formBuilder.group({
    username: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
    password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]]
  });

  constructor(private formBuilder: FormBuilder,
    private loginService: LoginService,
    private dialog: MatDialog,
    private router: Router,
    @Inject(DOCUMENT) private document: Document) {
    loginService.logOut();
  }

  ngOnInit() {
  }


  onSubmit() {
    let userLogin = {
      ...this.userLoginModel.value
    };

    this.loginService.login(userLogin)
      .subscribe(
        success => {
          localStorage.setItem('accessToken', success.encodedToken);
          this.router.navigate(['profile', success.userId]);
        },
        error => {
          console.log(error)
          this.dialog.open(AlertDialogComponent, {
            width: '40%',
            data: { title: 'Fail', content: error.error }
          });
        });
  }

  loginWithGoogle() {
    this.loginService.loginWithGoogle()
    .subscribe(
      result => console.log(result)
    );
  }

  get username() {
    return this.userLoginModel.get('username');
  }

  get password() {
    return this.userLoginModel.get('password');
  }

}
