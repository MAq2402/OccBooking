import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { UserModel } from '../auth/models/user.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  currentUser: UserModel;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.getCurrentUser();
    this.authService.newUserAnnounced$.subscribe(() => this.getCurrentUser());
  }

  private getCurrentUser() {
    this.authService.getCurrentUser().subscribe(user => this.currentUser = user);
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logOut() {
    this.authService.logOut();
  }
}
