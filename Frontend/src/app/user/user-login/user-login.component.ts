import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { Router } from '@angular/router';
import { UserFromLogin } from 'src/app/model/user';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  constructor(private authService: AuthService,
              private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
  }

  onLogin(loginForm: NgForm) {
    console.log(loginForm.value);
    this.authService.authUser(loginForm.value).subscribe(
      (response:UserFromLogin)=>{
        console.log(response);
        const userres=response;
        localStorage.setItem('username',userres.username);
        localStorage.setItem('token',userres.token);

      this.alertify.success('Login Successful');
      this.router.navigate(['/']);
      },

    );

  }

}
