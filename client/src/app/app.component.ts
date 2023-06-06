import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  users:any
  url = "https://localhost:5001/api/Users/"
  ngOnInit(): void {
    this.http.get(this.url).subscribe({
      next: res =>{
        this.users = res
        console.log("Fetching Data")
      },
      error: error=> console.log(error),
      complete:()=> console.log("Completed")
    })
  }
  constructor(private http:HttpClient) {
         
  }
  
  title = 'client';
}
