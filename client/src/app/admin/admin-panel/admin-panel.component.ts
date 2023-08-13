import { Component, OnInit } from '@angular/core';
import { TabDirective } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  activeTab?: TabDirective;


  constructor() { }

  ngOnInit(): void {
  }


  onTabActivated(data: TabDirective) {
    this.activeTab = data;

  }
}
