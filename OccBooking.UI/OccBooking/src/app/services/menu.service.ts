import { Injectable } from '@angular/core';
import { MenuModel } from '../models/menu.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IngredientModel } from '../models/ingredient.model';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private httpClient: HttpClient) { }

  getIngredients(): Observable<IngredientModel[]> {
    return this.httpClient.get<IngredientModel[]>(`${environment.WEB_API_ENDPOINT}ingredients`);
  }

  createMenu(placeId: string, model: MenuModel): Observable<void> {
    return this.httpClient.post<void>(`${environment.WEB_API_ENDPOINT}places/${placeId}/menus`, model);
  }

  getMenus(placeId: string): Observable<MenuModel[]> {
    return this.httpClient.get<MenuModel[]>(`${environment.WEB_API_ENDPOINT}places/${placeId}/menus`);
  }
}
