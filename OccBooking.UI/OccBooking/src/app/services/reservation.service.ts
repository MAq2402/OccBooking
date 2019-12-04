import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { occasionTypes } from '../shared/occasionTypes';
import { ReservationsModel } from '../models/reservations.model';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private http: HttpClient) { }

  findReservationsForDay(placeId: string, date: Date): Observable<ReservationsModel> {
      return this.http.post<ReservationsModel>(`${environment.WEB_API_ENDPOINT}places/${placeId}/reservations`, date);
  }
}
