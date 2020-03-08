import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;

  model: any = {};
  constructor(private http: HttpClient, public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    
  }


  login() {
    this.authService.login(this.model).subscribe( next => {
      this.alertify.success('Logged in Successfully');
    }, error => {
      this.alertify.error('Failed to Log in');
    }, () => {
      this.router.navigate(['/discover']);
    }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  registerToggle() {
    this.registerMode = true;
  }


  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;

  }
}
