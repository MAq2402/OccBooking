import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlaceComponent } from './place/place.component';
import { OwnerGuard } from './guards/owner.guard';

const routes: Routes = [
  { path: 'owner', component: PlaceComponent, canActivate: [OwnerGuard] }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [OwnerGuard]
})
export class OwnerRoutingModule { }
