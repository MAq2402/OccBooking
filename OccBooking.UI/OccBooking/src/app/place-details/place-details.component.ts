import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
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
  constructor(public dialog: MatDialog,
              private activatedRoute: ActivatedRoute,
              private placeService: PlaceService,
              private router: Router) { }

  ngOnInit(): void {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');

    this.getPlace();
   }

   navigateToMakeReservation(): void {
    this.router.navigate([`place/${this.placeId}/reservation`]);
  }

  private getPlace() {
    this.placeService.getPlace(this.placeId).subscribe(place => {
      this.place = place;
      if (this.place.image) {
        this.place.image = 'data:image/png;base64,' + place.image;
      } else {
        this.place.image = require('../../assets/default-image.jpg');
      }
      console.log(this.place);
      this.place.occasionTypesMaps = this.placeService.mapToOccasionTypeMap(this.place.occasionTypes);
    });
  }
}
