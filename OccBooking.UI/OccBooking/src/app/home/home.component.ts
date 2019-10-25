import { Component, OnInit } from '@angular/core';
import { PlaceService } from '../owner/services/place.service';
import { PlaceModel } from '../owner/models/place.model';
import { SidenavService } from './services/sidenav.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  places: PlaceModel[];

  constructor(private placeService: PlaceService, private sidenavService: SidenavService) { }

  ngOnInit() {
    this.placeService.getPlaces().subscribe(places => this.places = places);

    this.sidenavService.filterAnnounced$.subscribe(filterModel => {
      console.log(filterModel);
      this.placeService.getPlaces(filterModel).subscribe(places => this.places = places);
    });
  }

}
