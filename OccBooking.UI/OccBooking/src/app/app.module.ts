import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { AuthModule } from './auth/auth.module';
import { SharedModule } from './shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { OwnerModule } from './owner/owner.module';
import { SpinnerComponent } from './spinner/spinner.component';
import { SidenavComponent } from './home/sidenav/sidenav.component';
import { PlaceCardComponent } from './home/place-card/place-card.component';
import { PlaceDetailsComponent } from "./place-details/place-details.component";
import { CreateMenuDialogComponent } from './owner/place-management/create-menu-dialog/create-menu-dialog.component';
import { CreateOptionDialogComponent } from './owner/place-management/create-option-dialog/create-option-dialog.component';
import { CreateHallDialogComponent } from './owner/place-management/create-hall-dialog/create-hall-dialog.component';
import { CreateMealDialogComponent } from './owner/place-management/menu-section/create-meal-dialog/create-meal-dialog.component';
import { MenuDetailsComponent } from './owner/place-management/menu-section/menu-details/menu-details.component';
import { MakeReservationComponent } from './place-details/make-reservation/make-reservation.component';
import { ReservationDetailsComponent } from './owner/place-management/calendar-section/reservation-details/reservation-details.component';
import { MakeDecisionComponent } from './owner/reservations/make-decision/make-decision.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    SpinnerComponent,
    SidenavComponent,
    PlaceCardComponent,
    PlaceDetailsComponent,
    MakeReservationComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule,
    AuthModule,
    SharedModule,
    HttpClientModule,
    OwnerModule,
    AppRoutingModule
  ],
  schemas: [NO_ERRORS_SCHEMA],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [CreateMenuDialogComponent,
    CreateOptionDialogComponent,
    CreateHallDialogComponent,
    CreateMealDialogComponent,
    ReservationDetailsComponent,
    MenuDetailsComponent,
    MakeDecisionComponent]
})
export class AppModule { }
