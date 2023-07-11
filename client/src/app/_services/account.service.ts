import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http:HttpClient) { }

  private baseUrl = environment.apiUrl; 
  private currentUserSource = new BehaviorSubject<User | null>(null)
  currentUser$ = this.currentUserSource.asObservable();

  login(model:any){
    return this.http.post<User>(this.baseUrl +'account/login', model).pipe(
      map((res:User )=>{
        const user = res;
        if(user){
        localStorage.setItem('user',JSON.stringify(user));
        this.setCurrentUser(user)
        }
      })
    )
  }
  register(model:any){
    return this.http.post<User>(this.baseUrl + 'account/register',model).pipe(
      map(user => {
        if(user){
          if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user)
          }
        }
        return user
      })
    )
  }
  logout(){
    localStorage.removeItem('user')
    this.currentUserSource.next(null)
  }
  setCurrentUser(user:User){
    this.currentUserSource.next(user)
  }


  
}
