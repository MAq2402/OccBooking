import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { ReservationRequestService } from 'src/app/services/reservation-request.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { MakeDecisionComponent } from './make-decision/make-decision.component';
import { ClientModel } from 'src/app/models/client.model';

export class ReservationModel {
  id: string;
  date: Date;
  client: ClientModel;
  cost: number;
  status: string;
  occasion: string;
  amountOfPeople: number;
  placeId: string;
  placeName: string;
  isRejected: boolean;
  isAccepted: boolean;
  isAnswered: boolean;
}

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
        console.log(reservations);
        this.dataSource = new MatTableDataSource(reservations);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
    });
  }

  getStatus(reservationRequest: ReservationModel): string {
    return this.reservationRequestService.getStatus(reservationRequest);
  }

  getOccasionType(reservationRequest: ReservationModel): string {
    return this.reservationRequestService.getOccasionType(reservationRequest);
  }
}
