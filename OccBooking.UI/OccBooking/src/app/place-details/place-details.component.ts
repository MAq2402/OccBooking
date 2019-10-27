import { Component, OnInit } from '@angular/core';
import { MakeReservationDialogComponent } from './make-reservation-dialog/make-reservation-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.component.html',
  styleUrls: ['./place-details.component.scss']
})
export class PlaceDetailsComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void { }

  openMakeReservationDialog(): void {
    const dialogRef = this.dialog.open(MakeReservationDialogComponent, {
      width: '250px',
      // data: {name: this.name, animal: this.animal}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }

}
