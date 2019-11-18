import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { HallModel } from 'src/app/models/hall.model';

@Component({
  selector: 'app-create-hall-dialog',
  templateUrl: './create-hall-dialog.component.html',
  styleUrls: ['./create-hall-dialog.component.scss']
})
export class CreateHallDialogComponent implements OnInit {
  formGroup: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CreateHallDialogComponent>,
    private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      name: ['', Validators.required],
      capacity: ['', Validators.required]
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  submit() {
    const model: HallModel = {
      id: null,
      name: this.formGroup.controls['name'].value,
      capacity: this.formGroup.controls['capacity'].value,
      placeId: null,
      joins: null,
    };

    this.dialogRef.close(model);
  }

}
