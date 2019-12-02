import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MenuDetailsComponent } from '../../menu-section/menu-details/menu-details.component';
import { MenuModel } from 'src/app/models/menu.model';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.scss']
})
export class ReservationDetailsComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<MenuDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public date: Date) { }

  ngOnInit() {
  }

  close(): void {
    this.dialogRef.close();
  }
}
