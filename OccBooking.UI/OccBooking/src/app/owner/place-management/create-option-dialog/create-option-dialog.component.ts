import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AdditionalOptionModel } from 'src/app/models/additional-option.model';

@Component({
  selector: 'app-create-option-dialog',
  templateUrl: './create-option-dialog.component.html',
  styleUrls: ['./create-option-dialog.component.scss']
})
export class CreateOptionDialogComponent implements OnInit {

  formGroup: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<CreateOptionDialogComponent>,
    private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      name: ['', Validators.required],
      cost: ['', Validators.required],
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  submit() {
    const model: AdditionalOptionModel = {
      name: this.formGroup.controls['name'].value,
      cost: this.formGroup.controls['cost'].value
    };

    this.dialogRef.close(model);
  }
}
