import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isUserLoggedIn: boolean = false;
  isNavbarCollapseToggled: boolean = false;

  toggleNavbarCollapse: Function = () => this.isNavbarCollapseToggled = !this.isNavbarCollapseToggled

  constructor() { }

  ngOnInit(): void { }

}
