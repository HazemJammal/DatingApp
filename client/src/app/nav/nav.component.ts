import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/User';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}

  constructor(public accountService:AccountService) { }

  ngOnInit(): void {
  }

  username:any
  user = this.accountService.currentUser$.subscribe({
    next: res=> this.username = res?.username
  })

  login(){
    this.accountService.login(this.model).subscribe({
      next: res =>{
        console.log(res)
      },
      error: err => console.log(err)
    })
  }

  logout(){
    this.accountService.logout();
  }

}
