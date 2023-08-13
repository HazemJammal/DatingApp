import { Component, OnInit } from '@angular/core';
import {  ToastrService } from 'ngx-toastr';
import { PhotoToVerify } from 'src/app/_models/photoToVerify';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.css']
})
export class PhotoManagementComponent implements OnInit {


  photos:PhotoToVerify[] = []
  constructor(private adminService:AdminService, private toast:ToastrService) { }

  ngOnInit(): void {
    this.loadPhotos()
  }

  loadPhotos(){
    this.adminService.getPhotoToVerify().subscribe({
      next: photos => this.photos = photos
    })
  }

  verifyPhoto(photo:PhotoToVerify,decision:string){
    this.adminService.verifyPhoto(photo.id,decision).subscribe({
      next: ()=>{
        this.photos = this.photos.filter(x => x !== photo)
        this.toast.success("Successfully updated")
      }
    })
  }
  


}
