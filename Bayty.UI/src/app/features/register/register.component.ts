import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms'
import { Gender } from 'src/app/core/enums/Gender';
import { Role } from 'src/app/core/enums/Role';

import { User } from 'src/app/core/models/User';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  user: User = new User();

  gender = Gender;
  enumKeys: string[] = [];

  constructor(private fb: FormBuilder, private _http:HttpClient) {
    this.enumKeys = Object.keys(this.gender)
    console.log(this.enumKeys);
  }

  userForm = this.fb.group({
    profileImage: [''],
    firstName: new FormControl('', [Validators.min(3)]),
    lastName: new FormControl('', [Validators.min(3)]),
    email: new FormControl('', [Validators.email]),
    phoneNumber: new FormControl('', [Validators.min(3)]),
    address: [''],
    password: [''],
    ImagePath: ['dasdad4a5@_sda']
    // age: [0],
    // gender: [Gender.Male]
  });

  get firstName() { return this.userForm.get('firstName')}

  /* changeGender: Function = (event: any): void => this.userForm.value.gender = event.target.value; */
  
  printUser(e: Event) 
  {
    e.preventDefault();
    this._http.post<any>('https://localhost:7094/account/register', {accountType: "Personal",  ...this.userForm.value})
    .subscribe(next => {
      console.log(next);
    }, error => {
      console.log(error);
    });

    console.log(this.userForm.value);
  }

  ngOnInit(): void {
    console.log(this.user);
  }

  onImgSelected: Function = (event: any): void => {
    if (event.target.files) {
      let reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (e: any) => (this.userForm.value.profileImage = e.target.result);
    }
  }

}
