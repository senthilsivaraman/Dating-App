import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import {JwtModule} from '@auth0/angular-jwt';


import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './Members//member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { AuthGuard} from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './Members/member-card/member-card.component';
import { MemberDetailComponent } from './Members/member-detail/member-detail.component';


export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    ListsComponent,
    MessagesComponent,
    MemberCardComponent,
    MemberDetailComponent
  ],
  imports: [
   BrowserModule,
   HttpClientModule,
   FormsModule,
   BsDropdownModule.forRoot(),
   TabsModule.forRoot(),
   BrowserAnimationsModule,
   RouterModule.forRoot(appRoutes),
   JwtModule.forRoot({
    config: {
      tokenGetter,
      whitelistedDomains: ['localhost:5000'],
      blacklistedRoutes: ['localhost:5000/api/auth']
    }
   })
  ],
  providers: [
    ErrorInterceptorProvider,
    AuthService,
    AlertifyService,
    UserService,

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
