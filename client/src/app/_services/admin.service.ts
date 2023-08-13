import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { PhotoToVerify } from '../_models/photoToVerify';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get<User[]>(this.baseUrl + 'admin/users-with-roles');
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.http.put<string[]>(this.baseUrl + 'admin/edit-roles/'
      + username + '?roles=' + roles, {});
  }
  getPhotoToVerify(){
    return this.http.get<PhotoToVerify[]>(this.baseUrl + 'admin/photos-to-moderate')
  }
  verifyPhoto(id: number, decision: string) {
    return this.http.put(this.baseUrl + 'admin/photos-to-verify/'+id+'?decision='+decision,{})
  }
}