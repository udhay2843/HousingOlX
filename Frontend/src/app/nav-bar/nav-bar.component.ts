import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  loggedinUser: string;
  constructor(private alertify: AlertifyService,private router: Router) { }

  ngOnInit() {

  }

  loggedin()
  {
   this.loggedinUser = localStorage.getItem('username');
  //console.log(this.loggedinUser);
    return this.loggedinUser;

  }


  onLogout() {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    this.router.navigate(['/']);
    this.alertify.success("You are logged out !");
  }

}
