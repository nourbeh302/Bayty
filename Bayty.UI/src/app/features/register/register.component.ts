import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'
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

  constructor(private fb: FormBuilder, private _http: HttpClient) {
    this.enumKeys = Object.keys(this.gender)
    console.log(this.enumKeys);
  }

  registerForm: FormGroup = new FormGroup({
    profileImage: new FormGroup(this.user.profileImage),
    firstName: new FormControl(this.user.firstName, Validators.compose([
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(32),
    ])),
    lastName: new FormControl(this.user.lastName, Validators.compose([
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(32),
    ])),
    email: new FormControl(this.user.email, Validators.compose([
      Validators.required,
      Validators.email,
    ])),
    password: new FormControl(this.user.password, Validators.compose([
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
    ])),
    phoneNumber: new FormControl(this.user.phoneNumber, Validators.compose([
      Validators.required,
      Validators.minLength(11),
      Validators.maxLength(11),
    ])), /* ^01[0125][0-9]{8}$ Phone number regex */
    address: new FormControl(this.user.address)
  })

  get firstName() { return this.registerForm.get('firstName') }
  get lastName() { return this.registerForm.get('lastName') }
  get email() { return this.registerForm.get('email') }
  get password() { return this.registerForm.get('password') }
  get phoneNumber() { return this.registerForm.get('phoneNumber') }
  get address() { return this.registerForm.get('address') }

  /* changeGender: Function = (event: any): void => this.registerForm.value.gender = event.target.value; */

  printUser(e: Event) {
    e.preventDefault();
    this._http.post<any>('https://localhost:7094/account/register', { accountType: "Personal", ...this.registerForm.value })
      .subscribe(next => {
        console.log(next);
      }, error => {
        console.log(error);
      });

    console.log(this.registerForm.value);
  }

  ngOnInit(): void {
    console.log(this.registerForm.value.profileImage);
  }

  onImgSelected: Function = (event: any): void => {
    if (event.target.files) {
      let reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (e: any) => (this.registerForm.value.profileImage = e.target.result);
    }
  }

}
