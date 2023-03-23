import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { HomeFilterComponent } from './components/home-filter/home-filter.component';
import { CardsContainerComponent } from './components/cards-container/cards-container.component';


@NgModule({
  declarations: [
    HomeComponent,
    HomeFilterComponent,
    CardsContainerComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
