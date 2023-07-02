import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/User';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}

  constructor(public accountService:AccountService,private route: Router, private toast:ToastrService) { }

  ngOnInit(): void {
  }


  login(){
    this.accountService.login(this.model).subscribe({
      next: ()=> {
      this.route.navigateByUrl('/members')   
      this.toast.success("Logged in Successfully")
    },
      error: err => {
      }
    })
  }

  logout(){
    this.model = null
    this.accountService.logout();
    this.route.navigateByUrl('/')
  }

}
