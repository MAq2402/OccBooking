import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { HomeComponent } from './home/home.component';
import { PlaceDetailsComponent } from './place-details/place-details.component';
import { MakeReservationComponent } from './place-details/make-reservation/make-reservation.component';


const routes: Routes = [
  {
    component: HomeComponent,
    path: ''
  },
  {
    component: LoginComponent,
    path: 'login'
  },
  {
    component: RegisterComponent,
    path: 'register'
  },
  {
    component: PlaceDetailsComponent,
    path: 'place/:id'
  },
  {
    component: MakeReservationComponent,
    path: 'place/:id/reservation'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      routes,
      { enableTracing: true }
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
