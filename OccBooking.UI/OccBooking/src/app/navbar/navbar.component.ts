import { Component, OnInit, OnChanges, SimpleChanges, AfterViewInit } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { UserModel } from '../auth/models/user.model';
import { PlaceModel } from '../owner/models/place.model';
import { PlaceService } from '../owner/services/place.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, AfterViewInit {

  currentUser: UserModel;
  places: PlaceModel[];
  menuData: any;

  constructor(private authService: AuthService, private placeService: PlaceService) { }

  ngOnInit() {
    this.menuData = {
      menuItems: [
        {code: '1', name: 'first'},
        {code: '2', name: 'second'}
      ]
    };
    this.getCurrentUser();
    this.authService.newUserAnnounced$.subscribe(() => {
      this.getCurrentUser();
    });
  }

  private getCurrentUser() {
    this.authService.getCurrentUser().subscribe(user => {
      this.currentUser = user;
      this.placeService.getPlacesByOwner(this.currentUser.ownerId).subscribe(places => {
        this.places = places;
        console.log(places);
        console.log(this.places);
      });
    });
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logOut() {
    this.authService.logOut();
  }

  placeClicked(place: PlaceModel) {
    console.log(place);
  }

  ngAfterViewInit(): void {
    // this.authService.getCurrentUser().subscribe(user => {
    //   this.currentUser = user;
    //   this.placeService.getPlacesByOwner(this.currentUser.ownerId).subscribe(places => {
    //     this.places = places;
    //     console.log(places);
    //     console.log(this.places);
    //   });
    // });
  }
}
