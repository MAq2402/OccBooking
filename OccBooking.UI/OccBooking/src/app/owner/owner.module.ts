import { NgModule } from '@angular/core';
import { PlaceComponent } from './place/place.component';
import { SharedModule } from '../shared/shared.module';
import { OwnerRoutingModule } from './owner.routing.module';



@NgModule({
  declarations: [PlaceComponent],
  imports: [
    SharedModule,
    OwnerRoutingModule
  ],
  exports: [
    OwnerRoutingModule
  ],
})
export class OwnerModule { }
