import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './Members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './Members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { MemberEditComponent } from './Members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ListsResolver } from './_resolver/lists.resolver';

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'discover', component: MemberListComponent,
                 resolve: {users: MemberListResolver}},
            { path: 'discover/:id', component: MemberDetailComponent,
                resolve: {user: MemberDetailResolver}},
            { path: 'member/edit', component: MemberEditComponent,
                resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges]},
            { path: 'matches', component: ListsComponent},
            { path: 'messages', component: MessagesComponent},
            { path: 'lists', component: ListsResolver, resolve: {users: ListsResolver}},
        ]
    },
    {path: '**', redirectTo: 'home', pathMatch: 'full'}

];
