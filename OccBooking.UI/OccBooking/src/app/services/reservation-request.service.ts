import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ReservationRequestModel } from '../models/reservation-request.model';
import { ReservationModel } from '../owner/reservations/reservations.component';

@Injectable({
  providedIn: 'root'
})
export class ReservationRequestService {

  constructor(private http: HttpClient) { }

  getStatus(reservationRequest: ReservationModel) {
    if (reservationRequest.isAccepted) {
      return 'Zaakceptowana';
    } else if (reservationRequest.isRejected) {
      return 'Odrzucona';
    } else {
      return 'W oczekiwaniu';
    }
  }

  getOccasionType(reservationRequest: ReservationModel) {
    return reservationRequest.occasion === 'Wedding' ? 'Wesele' : 'Pogrzeb';
  }

  makeReservationRequest(placeId: string, model: ReservationRequestModel): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}places/${placeId}/reservationRequests`, model);
  }

  getReservationReqeusts(ownerId: string): Observable<ReservationModel[]> {
    return this.http.get<ReservationModel[]>(`${environment.WEB_API_ENDPOINT}owners/${ownerId}/reservationRequests`);
  }

  reject(id: string): Observable<any> {
    return this.http.put<any>(`${environment.WEB_API_ENDPOINT}reservationRequests/${id}/reject`, {});
  }

  accept(id: string, hallIds: string[]): Observable<any> {
    return this.http.put<any>(`${environment.WEB_API_ENDPOINT}reservationRequests/${id}/accept`, hallIds);
  }
}
