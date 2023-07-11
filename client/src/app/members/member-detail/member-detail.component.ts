import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/Member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member:Member|undefined
  constructor(private memberService:MembersService, private router:ActivatedRoute, private route: Router) { }

  galleryOptions: NgxGalleryOptions[] = []

  galleryImages: NgxGalleryImage[] = []

  ngOnInit(): void {
    this.loadMemberDetails();
    this.galleryOptions = [
      {
        width:'500px',
        height:'500px',
        imagePercent:100,
        thumbnailsColumns:4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview:false
      }
    ]
  }
  getImages(){
    if(!this.member) return [];
    const imageUrls = [];
    for(const photo of this.member.photos){
      imageUrls.push({
        small:photo.url,
        medium:photo.url,
        big:photo.url
      })
    }
    return imageUrls
  }


  loadMemberDetails(){
    const username = this.router.snapshot.paramMap.get('username')
    if(username)
    this.memberService.getMember(username).subscribe({
      next: user => {this.member = user
      this.galleryImages = this.getImages();}
    });
  }




}
