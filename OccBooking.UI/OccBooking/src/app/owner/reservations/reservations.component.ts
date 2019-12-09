import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { ReservationRequestService } from 'src/app/services/reservation-request.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { MakeDecisionComponent } from './make-decision/make-decision.component';
import { ReservationModel } from 'src/app/models/reservation.model';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {

  displayedColumns: string[] = ['placeName', 'date', 'client.email', 'cost', 'status', 'occasion', 'amountOfPeople', 'actions'];
  dataSource: MatTableDataSource<ReservationModel>;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private reservationRequestService: ReservationRequestService,
              private authService: AuthService,
              private dialog: MatDialog) { }

  ngOnInit() {
    this.getReservations();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  makeDecision(reservationRequest: ReservationModel) {
    const dialogReg = this.dialog.open(MakeDecisionComponent, { data: reservationRequest, width: '700px' });
    dialogReg.afterClosed().subscribe(() => this.getReservations());
  }

  private getReservations() {
    this.dataSource = new MatTableDataSource([]);
    this.authService.getCurrentUser().subscribe(user => {
      this.reservationRequestService.getReservationReqeusts(user.ownerId).subscribe(reservations => {
        for (const reservation of reservations) {
          this.getStatus(reservation);
          this.getOccasionType(reservation);
        }
        this.dataSource = new MatTableDataSource(reservations);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
    });
  }

  private getStatus(reservationRequest: ReservationModel) {
    reservationRequest.status = this.reservationRequestService.getStatus(reservationRequest);
  }

  private getOccasionType(reservationRequest: ReservationModel) {
    reservationRequest.occasion =  this.reservationRequestService.getOccasionType(reservationRequest);
  }
}
