import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  users:any
  url = "https://localhost:5001/api/Users/"
  ngOnInit(): void {
    this.setCurrentUser()
  }

  constructor(private accountService:AccountService) {
         
  }
  
  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(userString){
      const user = JSON.parse(userString)
      this.accountService.setCurrentUser(user)
    }
  }

}
