import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  name: FormControl = new FormControl()
  nameState: string = ''

  constructor() { }

  ngOnInit(): void { }

  validateName: Function = (name: string): string =>
    this.nameState = name.length < 3 ? 'Poor' : 'Great'
}