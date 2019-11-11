import { NgModule } from '@angular/core';
import { PlaceComponent } from './place/place.component';
import { SharedModule } from '../shared/shared.module';
import { OwnerRoutingModule } from './owner.routing.module';
import { PlaceManagementComponent } from './place-management/place-management.component';
import { CreateMenuDialogComponent } from './place-management/create-menu-dialog/create-menu-dialog.component';
import { CreateOptionDialogComponent } from './place-management/create-option-dialog/create-option-dialog.component';

@NgModule({
  declarations: [PlaceComponent, PlaceManagementComponent, CreateMenuDialogComponent, CreateOptionDialogComponent],
  imports: [
    SharedModule,
    OwnerRoutingModule
  ],
  exports: [
    OwnerRoutingModule
  ]
  ,
})
export class OwnerModule { }
