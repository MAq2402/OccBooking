import { Component, OnInit } from '@angular/core';
import { PlaceModel } from '../models/place.model';
import { SidenavService } from './services/sidenav.service';
import { PlaceService } from '../services/place.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  itemsToDislay = 5;
  places: PlaceModel[] = [];

  constructor(private placeService: PlaceService, private sidenavService: SidenavService) { }

  ngOnInit() {
    this.placeService.getPlaces().subscribe(places => this.places = places);

    this.sidenavService.filterAnnounced$.subscribe(filterModel => {
      this.placeService.getPlaces(filterModel).subscribe(places => this.places = places);
    });
  }

  onScroll() {
    this.itemsToDislay += 5;
  }

  getPlaces(): PlaceModel[] {
    return this.places.slice(0, this.itemsToDislay);
  }
}
