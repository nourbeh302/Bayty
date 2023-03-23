import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileDetailsRoutingModule } from './profile-details-routing.module';
import { ProfileDetailsComponent } from './profile-details.component';


@NgModule({
  declarations: [
    ProfileDetailsComponent
  ],
  imports: [
    CommonModule,
    ProfileDetailsRoutingModule
  ]
})
export class ProfileDetailsModule { }
