import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlaceModel } from '../../models/place.model';
import { CreateMenuDialogComponent } from './create-menu-dialog/create-menu-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { PlaceService } from 'src/app/services/place.service';
import { MenuService } from 'src/app/services/menu.service';
import { IngredientModel } from 'src/app/models/ingredient.model';
import { MenuModel } from 'src/app/models/menu.model';

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

    this.placeService.getPlace(this.placeId).subscribe(place => this.place = place);
    this.menuSerivce.getIngredients().subscribe(ingredients => this.ingredients = ingredients);
    this.getMenus();
  }

  createMenu() {
    const dialogRef = this.dialog.open(CreateMenuDialogComponent, {data: this.ingredients});

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.menuSerivce.createMenu(this.placeId, result).subscribe(() => this.getMenus());
      }
    });
  }

  private getMenus() {
    this.menuSerivce.getMenus(this.placeId).subscribe(menus => this.menus = menus);
  }
}
