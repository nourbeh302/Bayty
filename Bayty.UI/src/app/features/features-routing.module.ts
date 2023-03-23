import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

/* Pages */
import { LoginComponent } from './login/login.component';
import { ProfileDetailsComponent } from './profile-details/profile-details.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "register", component: RegisterComponent },
  { path: "user", component: ProfileDetailsComponent },
  { path: "", component: HomeComponent },
  { path: "**", component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeaturesRoutingModule { }
