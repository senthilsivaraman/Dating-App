import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  users: User[];
  likesParam: string;

  constructor(private authService: AuthService, private userService: UserService,
              private alertify: AlertifyService, private route: ActivatedRoute)  { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'];
    });
    this.likesParam = 'Likers';
  }

  loadUsers() {
    this.userService
      .getUsers(null, this.likesParam)
      .subscribe(
        (user: User[]) => {
           this.users = user;
            }, error => {
               this.alertify.error(error);
        });
  }

  resetLikes() {

  }

  resetLikers() {

  }

}
