import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { menuTypes } from 'src/app/shared/menuTypes';
import { MenuModel } from 'src/app/models/menu.model';
import { MealModel } from 'src/app/models/meal.model';
import { mealTypes } from 'src/app/shared/mealTypes';
import { IngredientModel } from 'src/app/models/ingredient.model';

@Component({
  selector: 'app-create-menu-dialog',
  templateUrl: './create-menu-dialog.component.html',
  styleUrls: ['./create-menu-dialog.component.scss']
})
export class CreateMenuDialogComponent implements OnInit {

  formGroup: FormGroup;
  types = menuTypes;
  mealTypes  = mealTypes;
  meals: MealModel[] = [];
  constructor(
    public dialogRef: MatDialogRef<CreateMenuDialogComponent>,
    private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      costPerPerson: ['', Validators.required],
      description: ['', Validators.required],
      // mealName: ['', Validators.required],
      // mealType: ['', Validators.required],
      // ingredients: ['', Validators.required],
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  submit() {
    const model: MenuModel = {
      name: this.formGroup.controls['name'].value,
      type: this.formGroup.controls['type'].value,
      costPerPerson: this.formGroup.controls['costPerPerson'].value,
      meals: []
    };

    this.dialogRef.close(model);
  }

}
