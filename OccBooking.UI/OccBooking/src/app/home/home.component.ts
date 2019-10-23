import { Component, OnInit } from '@angular/core';
import { PlaceService } from '../owner/services/place.service';
import { PlaceModel } from '../owner/models/place.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  places: PlaceModel[];

  constructor(private placeService: PlaceService) { }

  ngOnInit() {
    this.placeService.getPlaces().subscribe(places => this.places = places);
  }

}
