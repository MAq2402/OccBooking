import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlaceModel } from '../../models/place.model';
import { CreateMenuDialogComponent } from './create-menu-dialog/create-menu-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { PlaceService } from 'src/app/services/place.service';
import { MenuService } from 'src/app/services/menu.service';
import { IngredientModel } from 'src/app/models/ingredient.model';
import { MenuModel } from 'src/app/models/menu.model';
import { CreateOptionDialogComponent } from './create-option-dialog/create-option-dialog.component';
import { OccasionTypeMapModel } from 'src/app/models/occasion-type-map';
import { occasionTypes } from 'src/app/shared/occasionTypes';

@Component({
  selector: 'app-place-management',
  templateUrl: './place-management.component.html',
  styleUrls: ['./place-management.component.scss']
})
export class PlaceManagementComponent implements OnInit {

  place: PlaceModel;
  ingredients: IngredientModel[];
  menus: MenuModel[];
  placeId: string;
  dates: Date[];
  en: any;

  constructor(private activatedRoute: ActivatedRoute,
    private placeService: PlaceService,
    public dialog: MatDialog,
    private menuSerivce: MenuService) { }

  ngOnInit() {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');

    this.getPlace();
    this.menuSerivce.getIngredients().subscribe(ingredients => this.ingredients = ingredients);
    this.getMenus();

    this.en = {
      firstDayOfWeek: 0,
      dayNames: ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"],
      dayNamesShort: ["Pon", "Wto", "Śro", "Czw", "Pią", "Sob", "Nie"],
      dayNamesMin: ["Pn", "Wt", "Śr", "Cw", "Pi", "So", "Nd"],
      monthNames: ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"],
      monthNamesShort: ["Sty", "Lut", "Mar", "Kwie", "Maj", "Cze", "Lip", "Sie", "Wrz", "Paź", "Lis", "Gru"],
      today: 'Dzisiaj',
      clear: 'Wyczyść',
      dateFormat: 'mm/dd/yy',
      weekHeader: 'Wk'
    };
  }

  // WYDZIEL KALENDARZ DO KOMPONENTU
  createMenu() {
    const dialogRef = this.dialog.open(CreateMenuDialogComponent, { data: this.ingredients });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.menuSerivce.createMenu(this.placeId, result).subscribe(() => this.getMenus());
      }
    });
  }

  private getMenus() {
    this.menuSerivce.getMenus(this.placeId).subscribe(menus => this.menus = menus);
  }

  createAdditionalOption() {
    const dialogRef = this.dialog.open(CreateOptionDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.placeService.addOption(this.placeId, result).subscribe(() => this.getPlace());
      }
    });
  }

  private getPlace() {
    this.placeService.getPlace(this.placeId).subscribe(place => {
      this.place = place;
      this.place.occasionTypesMaps = this.mapToOccasionTypeMap(this.place.occasionTypes);
    });
  }

  private mapToOccasionTypeMap(occasionTypesAsStrings: string[]): OccasionTypeMapModel[] {
    let result: OccasionTypeMapModel[] = [];

    occasionTypesAsStrings.forEach(element => {
      const occasionTypeMap = occasionTypes.filter(o => o.value === element)[0];
      result.push(occasionTypeMap);
    });
    return result;
  }

  occasionTypeAdded(occasionType: OccasionTypeMapModel) {
    this.placeService.allowOccasionType(this.placeId, occasionType.value).subscribe(() => this.getPlace());
  }

  occasionTypeRemoved(occasionType: OccasionTypeMapModel) {
    this.placeService.disallowOccasionType(this.placeId, occasionType.value).subscribe(() => this.getPlace());
  }
}
