import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { UserModel } from '../auth/models/user.model';
import { PlaceModel } from '../models/place.model';
import { Router } from '@angular/router';
import { PlaceService } from '../services/place.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  currentUser: UserModel;
  places: PlaceModel[];
  menuData: any;

  constructor(private authService: AuthService, private placeService: PlaceService, private router: Router) { }

  ngOnInit() {
    this.menuData = {
      menuItems: [
        { code: '1', name: 'first' },
        { code: '2', name: 'second' }
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
      });
    });
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logOut() {
    this.authService.logOut();
  }

  navigateToPlace(placeId: string) {
    this.router.navigate(['/owner/place-management', placeId]);
  }
}
