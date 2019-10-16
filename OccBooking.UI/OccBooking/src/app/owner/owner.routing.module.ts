import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlaceComponent } from './place/place.component';

const routes: Routes = [
    {path: 'owner', component: PlaceComponent}
];

@NgModule({
    imports: [
      RouterModule.forChild(routes)
    ],
    exports: [
      RouterModule
    ]
  })
export class OwnerRoutingModule { }
