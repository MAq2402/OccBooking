import { NgModule } from '@angular/core';
import { PlaceComponent } from './place/place.component';
import { SharedModule } from '../shared/shared.module';
import { OwnerRoutingModule } from './owner.routing.module';
import { PlaceManagementComponent } from './place-management/place-management.component';

@NgModule({
  declarations: [PlaceComponent, PlaceManagementComponent],
  imports: [
    SharedModule,
    OwnerRoutingModule
  ],
  exports: [
    OwnerRoutingModule
  ],
})
export class OwnerModule { }
