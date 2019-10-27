import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-make-reservation-dialog',
  templateUrl: './make-reservation-dialog.component.html',
  styleUrls: ['./make-reservation-dialog.component.scss']
})
export class MakeReservationDialogComponent implements OnInit {

  model: any;
  constructor(
    public dialogRef: MatDialogRef<MakeReservationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

  ngOnInit() {
  }

  onCloseClick(): void {
    this.dialogRef.close();
  }

}
