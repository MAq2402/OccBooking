import { Component, OnInit } from '@angular/core';
import { CreateMenuDialogComponent } from '../create-menu-dialog/create-menu-dialog.component';
import { MenuService } from 'src/app/services/menu.service';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { MenuModel } from 'src/app/models/menu.model';
import { IngredientModel } from 'src/app/models/ingredient.model';
import { CreateMealDialogComponent } from './create-meal-dialog/create-meal-dialog.component';

@Component({
  selector: 'app-menu-section',
  templateUrl: './menu-section.component.html',
  styleUrls: ['./menu-section.component.scss']
})
export class MenuSectionComponent implements OnInit {

  placeId: string;
  menus: MenuModel[];
  ingredients: IngredientModel[];
  constructor(private menuService: MenuService, public dialog: MatDialog, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');

    this.getMenus()
    this.menuService.getIngredients().subscribe(ingredients => this.ingredients = ingredients);
  }

  createMenu() {
    const dialogRef = this.dialog.open(CreateMenuDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.menuService.createMenu(this.placeId, result).subscribe(() => this.getMenus());
      }
    });
  }

  addMeal(menuId: string) {
    const dialogRef = this.dialog.open(CreateMealDialogComponent, {data: this.ingredients});

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.menuService.addMeal(menuId, result).subscribe(() => this.getMenus());
      }
    });
  }

  private getMenus() {
    this.menuService.getMenus(this.placeId).subscribe(menus => this.menus = menus);
  }
}
