import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map,Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService:AccountService, private toast:ToastrService){}
  canActivate (): Observable<boolean> {
    return  this.accountService.currentUser$.pipe(
      map(user =>{
        if(user) return true
        
        this.toast.error("You are not allowed here!")
        return false
      })

    )
  }
  
}
