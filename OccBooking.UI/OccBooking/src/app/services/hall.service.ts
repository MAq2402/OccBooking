import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HallModel } from '../models/hall.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HallService {

  constructor(private http: HttpClient) { }

  getHalls(placeId: string): Observable<HallModel[]> {
    return this.http.get<HallModel[]>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls`);
  }

  createHall(placeId: string, model: HallModel): Observable<any> {
    return this.http.post<any>(`${environment.WEB_API_ENDPOINT}places/${placeId}/halls`, model);
  }
}
