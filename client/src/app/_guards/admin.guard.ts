import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { Toast, ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})

export class AdminGuard implements CanActivate {
  constructor(private accountService:AccountService, private toast:ToastrService) {
    
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {


    return this.accountService.currentUser$.pipe(
      map(user =>{
        if(!user) return false;
        
        if(user.roles.includes('Admin') || user.roles.includes('Moderator'))
          return true

        this.toast.error("You are not allowed here ")
        return false;
        
      })
    )
  }
  
}
