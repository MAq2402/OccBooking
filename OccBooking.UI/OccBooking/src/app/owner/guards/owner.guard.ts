import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthService } from 'src/app/auth/services/auth.service';

@Injectable()
export class OwnerGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) { }

    canActivate(): boolean {
        if (!this.authService.isLoggedIn()) {
            this.router.navigate(['']);
            return false;
        }
        return true;
    }
}
