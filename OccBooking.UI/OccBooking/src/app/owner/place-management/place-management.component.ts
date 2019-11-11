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
import { EnumType } from 'src/app/shared/enum-type';

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
  constructor(private activatedRoute: ActivatedRoute,
    private placeService: PlaceService,
    public dialog: MatDialog,
    private menuSerivce: MenuService) { }

  ngOnInit() {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');

    this.getPlace();
    this.menuSerivce.getIngredients().subscribe(ingredients => this.ingredients = ingredients);
    this.getMenus();
  }

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
    this.placeService.getPlace(this.placeId).subscribe(place => this.place = place);
  }

  occasionTypeAdded(occasionType: EnumType) {
    console.log(occasionType);
    this.placeService.allowOccasionType(this.placeId, occasionType.id).subscribe(() => this.getPlace());
  }

  occasionTypeRemoved(occasionType: EnumType) {
    console.log(occasionType);
    this.placeService.disallowOccasionType(this.placeId, occasionType.id).subscribe(() => this.getPlace());
  }
}
