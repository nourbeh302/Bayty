import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FeaturesRoutingModule } from './features/features-routing.module';

const routes: Routes = [
  { path: "", loadChildren: () => FeaturesRoutingModule },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
