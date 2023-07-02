import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
  model:any ={}
  constructor(private accountService:AccountService, private toast:ToastrService) { }

  @Output() cancelRegister = new EventEmitter();
  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next: () =>{        
      this.toast.success("Account Registerd Successfully")
      this.cancel()
    },
    error: err=> this.toast.error(err.error)
    })
  }
  cancel(){
  this.model = null
  this.cancelRegister.emit()
  }


}
