import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MealModel } from 'src/app/models/meal.model';
import { mealTypes } from 'src/app/shared/mealTypes';
import { IngredientModel } from 'src/app/models/ingredient.model';

@Component({
  selector: 'app-create-meal-dialog',
  templateUrl: './create-meal-dialog.component.html',
  styleUrls: ['./create-meal-dialog.component.scss']
})
export class CreateMealDialogComponent implements OnInit {
  formGroup: FormGroup;
  types = mealTypes;
  constructor(
    public dialogRef: MatDialogRef<CreateMealDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public ingredients: IngredientModel[],
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      type: ['', Validators.required],
      ingredients: ['', Validators.nullValidator]
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  submit() {
    const model: MealModel = {
      name: this.formGroup.controls['name'].value,
      description: this.formGroup.controls['description'].value,
      type: this.formGroup.controls['type'].value,
      ingredients: this.formGroup.controls['ingredients'].value,
    };

    this.dialogRef.close(model);
  }
}
