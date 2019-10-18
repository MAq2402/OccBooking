import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http'
import { Observable, of, Subject } from 'rxjs';
import { LoginCredentialsModel } from '../models/login-credentials.model';
import { tap } from 'rxjs/operators';
import { LoginResponseModel } from '../models/login-response.model';
import { RegisterModel } from '../models/register.model';
import { UserModel } from '../models/user.model';

const TOKEN = 'token';
const ROUTE = `${environment.WEB_API_ENDPOINT}auth/`;

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUser: UserModel;
  private newUserAnnouncedSource = new Subject<void>();

  newUserAnnounced$ = this.newUserAnnouncedSource.asObservable();

  constructor(private http: HttpClient) { }

  register(model: RegisterModel): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(`${ROUTE}register`, model).pipe(tap(token => {
      localStorage.setItem(TOKEN, token.authToken);
    }));
  }

  login(model: LoginCredentialsModel): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(`${ROUTE}login`, model).pipe(tap(token => {
      localStorage.setItem(TOKEN, token.authToken);
      this.announceNewUser();
    }));
  }

  private announceNewUser() {
    this.newUserAnnouncedSource.next();
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(TOKEN);
  }

  logOut() {
    localStorage.removeItem(TOKEN);
    this.currentUser = null;
  }

  getCurrentUser(): Observable<UserModel> {
    return this.currentUser ? of(this.currentUser) :
           this.http.get<UserModel>(`${ROUTE}user`).pipe(tap(res => this.currentUser = res));
  }
}
