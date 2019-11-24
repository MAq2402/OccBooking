import { Component, OnInit } from '@angular/core';
import { MakeReservationDialogComponent } from './make-reservation-dialog/make-reservation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { PlaceModel } from '../models/place.model';
import { PlaceService } from '../services/place.service';
declare var require: any;

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.component.html',
  styleUrls: ['./place-details.component.scss']
})
export class PlaceDetailsComponent implements OnInit {

  placeId: string;
  place: PlaceModel;
  constructor(public dialog: MatDialog, private activatedRoute: ActivatedRoute, private placeService: PlaceService) { }

  ngOnInit(): void {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');

    this.getPlace();
   }

  openMakeReservationDialog(): void {
    const dialogRef = this.dialog.open(MakeReservationDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  private getPlace() {
    this.placeService.getPlace(this.placeId).subscribe(place => {
      this.place = place;
      if (this.place.image) {
        this.place.image = 'data:image/png;base64,' + place.image;
      } else {
        this.place.image = require('../../assets/default-image.jpg');
      }

      // this.place.occasionTypesMaps = this.mapToOccasionTypeMap(this.place.occasionTypes); // do jakiegos serwisu to mapowanie
    });
  }

}
