import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
  model:any ={}
  constructor(private accountService:AccountService) { }

  @Output() cancelRegister = new EventEmitter();
  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next: res =>{ console.log(res)
      this.cancel()
    },
    error: err=> console.log(err)
    })
  }
  cancel(){
  this.model = null
  this.cancelRegister.emit()
  }


}
