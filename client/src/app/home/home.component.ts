import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }


  registerForm = false
  ngOnInit(): void {
  }

  showRegisterForm(){
    this.registerForm = !this.registerForm;
  }


}
