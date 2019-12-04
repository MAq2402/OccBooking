import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MenuDetailsComponent } from '../../menu-section/menu-details/menu-details.component';
import { ReservationService } from 'src/app/services/reservation.service';
import { ActivatedRoute } from '@angular/router';
import { ReservationsModel } from 'src/app/models/reservations.model';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.scss']
})
export class ReservationDetailsComponent implements OnInit {

  placeId: string;
  date: Date;
  reservation: ReservationsModel;
  constructor(
    public dialogRef: MatDialogRef<MenuDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private reservationService: ReservationService) { }

  ngOnInit() {
    this.date = this.data.date;
    this.placeId = this.data.placeId;
    console.log(this.placeId);
    this.reservationService.findReservationsForDay(this.placeId, this.date).subscribe(reservation => {
      console.log(reservation);
      if (!reservation.isEmpty && (!reservation.hallReservations || reservation.hallReservations.length === 0)) {
        this.close();
      }
      this.reservation = reservation;
    });
  }

  close(): void {
    this.dialogRef.close();
  }
}
