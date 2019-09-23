import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material/material.module';
import { CoreModule } from '../core/core.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms'
import { RouterModule } from '@angular/router';
import { MDBBootstrapModule } from 'angular-bootstrap-md';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaterialModule,
    CoreModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    MDBBootstrapModule.forRoot()
  ],
  exports: [
    MaterialModule,
    CommonModule,
    CoreModule,
    ReactiveFormsModule,
    FormsModule,
    MDBBootstrapModule
  ]
})
export class SharedModule { }
