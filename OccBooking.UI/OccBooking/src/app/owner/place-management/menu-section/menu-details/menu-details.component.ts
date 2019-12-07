import { Component, OnInit, Inject } from '@angular/core';
import { MenuModel } from 'src/app/models/menu.model';
import { MAT_DIALOG_DATA, MatDialogRef, MatTableDataSource } from '@angular/material';
import { menuTypes } from 'src/app/shared/menuTypes';
import { mealTypes } from 'src/app/shared/mealTypes';
import { MealModel } from 'src/app/models/meal.model';

@Component({
  selector: 'app-menu-details',
  templateUrl: './menu-details.component.html',
  styleUrls: ['./menu-details.component.scss']
})
export class MenuDetailsComponent implements OnInit {

  dataSource: MatTableDataSource<MealModel>;
  displayedColumns = ['name', 'description', 'type', 'ingredients'];
  constructor(
    public dialogRef: MatDialogRef<MenuDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public menu: MenuModel) { }

  ngOnInit() {
    this.dataSource = new MatTableDataSource(this.menu.meals);
  }

  close(): void {
    this.dialogRef.close();
  }

  getMenuType(type: number): string {
    return menuTypes.filter(t => t.id === type)[0].name;
  }

  getMealType(type: number): string {
    return mealTypes.filter(t => t.id === type)[0].name;
  }

  getIngredients(ingredients: string[]): string {
    return ingredients.join(',');
  }
}
