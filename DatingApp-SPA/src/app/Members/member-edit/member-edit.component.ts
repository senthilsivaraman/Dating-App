import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';


export interface RelationshipStatus {
  value: string;
  viewValue: string;
}

export interface LookingFor {
  value: string;
  viewValue: string;
}

export interface InterestedIn {
  value: string;
  viewValue: string;
}

export interface Religion {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})

export class MemberEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  user: User;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event:any) {
    if(this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  

  rstaus: RelationshipStatus[] = [
    {value: '0', viewValue: 'Single'},
    {value: '1', viewValue: 'In Relationship'},
    {value: '2', viewValue: 'Engaged'},
    {value: '3', viewValue: 'Widowed'},
    {value: '4', viewValue: 'Divorced'},
    {value: '5', viewValue: 'Single Mom / Dad'}
  ];

  rlooking: LookingFor[] = [
    {value: '0', viewValue: 'Relationship'},
    {value: '1', viewValue: 'Something Casual'},
    {value: '2', viewValue: 'Dont know yet'},
    {value: '3', viewValue: 'Marriage'},
  ];

  rInterest: InterestedIn[] = [
    {value: '0', viewValue: 'Male'},
    {value: '1', viewValue: 'Female'},
    {value: '2', viewValue: 'Other'},
    {value: '3', viewValue: 'Everyone'},
  ];

  rReligion: Religion[] = [
    {value: '0', viewValue: 'Buddhist'},
    {value: '1', viewValue: 'Christian'},
    {value: '2', viewValue: 'Hindu'},
    {value: '3', viewValue: 'Inter-Religion'},
    {value: '4', viewValue: 'Jain'},
    {value: '5', viewValue: 'Muslim'},
    {value: '5', viewValue: 'Parsi'},
    {value: '5', viewValue: 'Sikh'},
    {value: '5', viewValue: 'Other'}
  ];

  constructor(private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
  }

  updateUser() {
    console.log(this.user);
    this.alertify.success('Profile Updated successfully');
    this.editForm.reset(this.user); // this will reset form status i.e after saving the changes save button and information banner
                          // will go disabled by susing Viewchild here in oninit method
  }

}
