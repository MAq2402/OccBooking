import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HallModel } from '../models/hall.model';
import { environment } from 'src/environments/environment';
import { HallJoinModel } from '../models/hall-join.model';
import { FilterHallsModel } from '../models/filter-halls.model';

@Injectable({
  providedIn: 'root'
})
export class HallService {

  constructor(private http: HttpClient) { }

  getHalls(placeId: string): Observable<HallModel[]> {
    return this.http.get<HallModel[]>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls`);
  }

  filterHalls(placeId: string, date: Date): Observable<HallModel[]> {
    const model: FilterHallsModel = {date: date}
    return this.http.post<HallModel[]>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls/filter`, model);
  }

  getHall(hallId: string): Observable<HallModel> {
    return this.http.get<HallModel>(`${environment.WEB_API_ENDPOINT}halls/${hallId}`);
  }

  createHall(placeId: string, model: HallModel): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls`, model);
  }

  makeEmptyReservations(placeId: string, datesToSend: Date[]): Observable<any> {
    return this.http.put<any>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls`, datesToSend);
  }

  updateHallJoins(hallId: string, hallJoins: HallJoinModel[]) {
    return this.http.put<any>(`${environment.WEB_API_ENDPOINT}halls/${hallId}/joins`, hallJoins);
  }
}
