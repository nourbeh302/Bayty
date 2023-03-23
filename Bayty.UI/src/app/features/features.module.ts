import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FeaturesRoutingModule } from './features-routing.module';

import { HomeModule } from './home/home.module';
import { LoginModule } from './login/login.module';
import { RegisterModule } from './register/register.module';
import { ProfileDetailsModule } from './profile-details/profile-details.module';
import { NotFoundModule } from './not-found/not-found.module';

import { LoaderComponent } from '../shared/components/loader/loader.component';


@NgModule({
  declarations: [
    LoaderComponent
  ],
  imports: [
    CommonModule,
    FeaturesRoutingModule,
    HomeModule,
    LoginModule,
    RegisterModule,
    ProfileDetailsModule,
    NotFoundModule
  ],
  providers: [
  ]
})
export class FeaturesModule { }
