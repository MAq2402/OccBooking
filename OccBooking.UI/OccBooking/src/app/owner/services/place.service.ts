import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http'
import { PlaceModel } from '../models/place.model';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PlaceService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  createPlace(ownerId: string, model: PlaceModel): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}${ownerId}/places`, model);
  }

  getPlaces(): Observable<PlaceModel[]> {
    return this.http.get<PlaceModel[]>(`${environment.WEB_API_ENDPOINT}places`);
  }
}
