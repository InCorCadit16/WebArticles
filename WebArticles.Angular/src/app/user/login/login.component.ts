import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { LoginService } from 'src/app/services/login-service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/app/shared/alert-dialog/alert-dialog.component';
import { AuthService, GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { ExternalSignInQuery } from 'src/app/data-model/dto/external-signin-query.dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService, AuthService]
})
export class LoginComponent implements OnInit {
  userLoginModel = this.formBuilder.group({
    username: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
    password: ['',[Validators.required, Validators.minLength(8), Validators.maxLength(30)]]
  });

  constructor(private formBuilder: FormBuilder,
              private loginService: LoginService,
              private authService: AuthService,
              private dialog: MatDialog,
              private router: Router) {
                loginService.logOut();
               }

  ngOnInit() {
  }


  onSubmit() {
    let userLogin = {
      ...this.userLoginModel.value
    };

    this.loginService.login(userLogin)
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
  }

  loginWithGoogle() {
    let platform = GoogleLoginProvider.PROVIDER_ID;
    this.authService.signIn(platform).then(
      response => {
        let query = new ExternalSignInQuery();

        query.externalId = response.id;
        query.userName = response.name;
        query.email = response.email;
        query.firstName = response.firstName;
        query.lastName = response.lastName;
        query.profilePickLink = response.photoUrl;
        query.provider = response.provider;

        this.loginService.loginExternal(query)
        .subscribe(
          succeeded => {
            localStorage.setItem('accessToken', succeeded.encodedToken)
            this.router.navigate(['main']);
          },
          failed => {
            this.dialog.open(AlertDialogComponent, {
              width: '40%',
              data: { title: 'Fail', content: failed.error.errorMessage }
            });
          }
        )
      }
    )
  }

  loginWithFacebook() {
    let platform = FacebookLoginProvider.PROVIDER_ID;
    this.authService.signIn(platform).then(
      response => {
        let query = new ExternalSignInQuery();

        if (response.email === null) {
          this.dialog.open(AlertDialogComponent, {
            width: '40%',
            data: { title: 'Fail', content: "Your account must have an email" }
          });
          return;
        }

        query.externalId = response.id;
        query.email = response.email;
        query.userName = response.email.substr(0, response.email.indexOf('@'));
        query.firstName = response.firstName;
        query.lastName = response.lastName;
        query.profilePickLink = response.photoUrl;
        query.provider = response.provider;

        this.loginService.loginExternal(query)
        .subscribe(
          succeeded => {
            localStorage.setItem('accessToken', succeeded.encodedToken)
            this.router.navigate(['main']);
          },
          failed => {
            this.dialog.open(AlertDialogComponent, {
              width: '40%',
              data: { title: 'Fail', content: failed.error.errorMessage }
            });
          }
        )
      }
    )
  }

  get username() {
    return this.userLoginModel.get('username');
  }

  get password() {
    return this.userLoginModel.get('password');
  }

}
