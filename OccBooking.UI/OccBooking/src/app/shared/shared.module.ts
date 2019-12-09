import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material/material.module';
import { CoreModule } from '../core/core.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms'
import { RouterModule } from '@angular/router';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { CalendarModule } from 'primeng/calendar'; import { CalendarSectionComponent } from '../owner/place-management/calendar-section/calendar-section.component';
import { MenuSectionComponent } from '../owner/place-management/menu-section/menu-section.component';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

@NgModule({
  declarations: [
    CalendarSectionComponent,
    MenuSectionComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    CoreModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    MDBBootstrapModule.forRoot(),
    CalendarModule,
    InfiniteScrollModule
  ],
  exports: [
    MaterialModule,
    CommonModule,
    CoreModule,
    ReactiveFormsModule,
    FormsModule,
    MDBBootstrapModule,
    CalendarModule,
    CalendarSectionComponent,
    MenuSectionComponent,
    InfiniteScrollModule
  ],
  providers: [
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 2500 } }
  ]
})
export class SharedModule { }
