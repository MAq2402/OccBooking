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
import { MakeReservationDialogComponent } from './place-details/make-reservation-dialog/make-reservation-dialog.component';
import { CreateMenuDialogComponent } from './owner/place-management/create-menu-dialog/create-menu-dialog.component';
import { CreateOptionDialogComponent } from './owner/place-management/create-option-dialog/create-option-dialog.component';
import { CreateHallDialogComponent } from './owner/place-management/create-hall-dialog/create-hall-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    SpinnerComponent,
    SidenavComponent,
    PlaceCardComponent,
    PlaceDetailsComponent,
    MakeReservationDialogComponent
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
  entryComponents: [MakeReservationDialogComponent, CreateMenuDialogComponent, CreateOptionDialogComponent, CreateHallDialogComponent]
})
export class AppModule { }
