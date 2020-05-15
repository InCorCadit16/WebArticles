import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login-service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user-service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [ LoginService, UserService ]
})
export class HeaderComponent implements OnInit {
  pictureLink;
  expanded = false;

  constructor(public userService: UserService, public loginService: LoginService, private router: Router) {

  }

  ngOnInit() {
    if (this.loginService.isLoggedIn()) {
      this.userService.getProfilePickLink(this.loginService.getUserId())
      .subscribe(link => {
        this.pictureLink = link.profilePick;
      });
    }

  }

  onLogOut() {
    this.loginService.logOut();
    this.router.navigate(['login']);
  }

}
