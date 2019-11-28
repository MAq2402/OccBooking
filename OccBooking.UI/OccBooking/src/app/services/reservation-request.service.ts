import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ReservationRequestModel } from '../models/reservation-request.model';

@Injectable({
  providedIn: 'root'
})
export class ReservationRequestService {

  constructor(private http: HttpClient) { }

  makeReservationRequest(placeId: string, model: ReservationRequestModel): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}places/${placeId}/reservationRequests`, model);
  }
}
