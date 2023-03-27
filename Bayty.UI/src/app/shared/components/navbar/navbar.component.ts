import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isUserLoggedIn: boolean = false
  baseIconPath: string = '/assets/icons/'

  navigationLinks: Object[] = [
    { routerRedirect: 'notifications', iconPath: `${this.baseIconPath}camera_24.svg` },
    { routerRedirect: 'chat', iconPath: `${this.baseIconPath}camera_24.svg` },
    { routerRedirect: 'payment', iconPath: `${this.baseIconPath}camera_24.svg` },
    { routerRedirect: 'user', iconPath: `${this.baseIconPath}camera_24.svg` }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
