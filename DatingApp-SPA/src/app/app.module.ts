import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, ButtonsModule, PaginationModule } from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { NgxGalleryModule } from 'ngx-gallery';
import {JwtModule} from '@auth0/angular-jwt';
import { FileUploadModule } from 'ng2-file-upload';


import { AppComponent } from './app.component';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { appRoutes } from './routes';
import { AuthGuard} from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import {TimeAgoPipe} from 'time-ago-pipe';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './Members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberCardComponent } from './Members/member-card/member-card.component';
import { MemberDetailComponent } from './Members/member-detail/member-detail.component';
import { MemberEditComponent } from './Members/member-edit/member-edit.component';
import { PhotoEditorComponent } from './Members/photo-editor/photo-editor.component';
import { ListsResolver } from './_resolver/lists.resolver';





export function tokenGetter() {
  return localStorage.getItem('token');
}

export class CustomHammerConfig extends HammerGestureConfig  {
  overrides = {
      pinch: { enable: false },
      rotate: { enable: false }
  };
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
    MemberDetailComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TimeAgoPipe
  ],
  imports: [
   BrowserModule,
   HttpClientModule,
   FormsModule,
   ButtonsModule.forRoot(),
   ReactiveFormsModule,
   BsDropdownModule.forRoot(),
   PaginationModule.forRoot(),
   TabsModule.forRoot(),
   ButtonsModule,
   BrowserAnimationsModule,
   RouterModule.forRoot(appRoutes),
   NgxGalleryModule,
   FileUploadModule,
   JwtModule.forRoot({
    config: {
      tokenGetter,
      whitelistedDomains: ['localhost:5000'],
      blacklistedRoutes: ['localhost:5000/api/auth']
    }
   })
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AuthGuard,
    PreventUnsavedChanges,
    AlertifyService,
    UserService,
    MemberDetailResolver,
    MemberListResolver,
    ListsResolver,
    MemberEditResolver,
    { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
