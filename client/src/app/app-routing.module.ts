import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { ErrorPageComponent } from './error-page/error-page.component';

const routes: Routes = [
{path:'',component:HomeComponent},
{path:'',runGuardsAndResolvers:'always',
  canActivate:[AuthGuard],
  children:[
    {path:'members',component:MemberListComponent},
    {path:'members/:id',component:MemberDetailComponent},
    {path:'messages',component:MessagesComponent},
    {path:'lists',component:ListsComponent},
  ]
},
{path:'**',component:ErrorPageComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
