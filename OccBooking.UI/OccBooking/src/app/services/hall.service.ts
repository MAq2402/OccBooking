import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HallModel } from '../models/hall.model';
import { environment } from 'src/environments/environment';
import { HallJoinModel } from '../models/hall-join.model';
import { FilterHallsModel } from '../models/filter-halls.model';
import { HallReservationModel } from '../models/hall-reservation.model';

@Injectable({
  providedIn: 'root'
})
export class HallService {

  constructor(private http: HttpClient) { }

  getHalls(placeId: string): Observable<HallModel[]> {
    return this.http.get<HallModel[]>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls`);
  }

  filterHalls(placeId: string, date: Date): Observable<HallModel[]> {
    const model: FilterHallsModel = { date: date }
    return this.http.post<HallModel[]>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls/filter`, model);
  }

  getHall(hallId: string): Observable<HallModel> {
    return this.http.get<HallModel>(`${environment.WEB_API_ENDPOINT}halls/${hallId}`);
  }

  createHall(placeId: string, model: HallModel): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls`, model);
  }

  makeEmptyPlaceReservations(placeId: string, datesToSend: Date[]): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls/reserve`, datesToSend);
  }

  makeEmptyHallReservations(hallId: string, datesToSend: Date[]): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}halls/${hallId}/reserve`, datesToSend);
  }

  updateHallJoins(hallId: string, hallJoins: HallJoinModel[]) {
    return this.http.put<any>(`${environment.WEB_API_ENDPOINT}halls/${hallId}/joins`, hallJoins);
  }

  getReservedDays(hallId: string): Observable<Date[]> {
    return this.http.get<Date[]>(`${environment.WEB_API_ENDPOINT}halls/${hallId}/reservedDays`);
  }

  getHallReservations(hallId: string): Observable<HallReservationModel[]> {
    return this.http.get<HallReservationModel[]>(`${environment.WEB_API_ENDPOINT}halls/${hallId}/hallReservations`);
  }
}
