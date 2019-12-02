import { Component, OnInit, Inject } from '@angular/core';
import { ReservationModel } from '../reservations.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { HallModel } from 'src/app/models/hall.model';
import { HallService } from 'src/app/services/hall.service';
import { ReservationRequestService } from 'src/app/services/reservation-request.service';
import { HallJoinModel } from 'src/app/models/hall-join.model';
export class HallChoice {
  hall: HallModel;
  choosed: boolean;
}
@Component({
  selector: 'app-make-decision',
  templateUrl: './make-decision.component.html',
  styleUrls: ['./make-decision.component.scss']
})
export class MakeDecisionComponent implements OnInit {

  halls: HallModel[] = [];
  hallsChoices: HallChoice[] = [];
  constructor(private hallService: HallService,
              private reservationService: ReservationRequestService,
              public dialogRef: MatDialogRef<MakeDecisionComponent>,
              @Inject(MAT_DIALOG_DATA) public reservationRequest: ReservationModel) { }

  ngOnInit() {
    this.hallService.filterHalls(this.reservationRequest.placeId, this.reservationRequest.date).subscribe(halls => {
      this.halls = halls;
      console.log(this.halls);
      for (const hall of this.halls) {
        this.hallsChoices.push({ hall, choosed: false });
      }
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  reject() {
    this.reservationService.reject(this.reservationRequest.id).subscribe(() => this.dialogRef.close());
  }

  accept() {
    this.reservationService.accept(this.reservationRequest.id,
      this.hallsChoices.filter(c => c.choosed).map(c => c.hall.id)).subscribe(() => this.dialogRef.close());
  }

  getSpinnerValue(): number {
    return (this.getCapacityOfChoosedHalls() / this.reservationRequest.amountOfPeople) * 100;
  }

  getCapacityOfChoosedHalls(): number {
    let result = 0;
    
    for (const choice of this.hallsChoices) {
      if (choice.choosed) {
        result += choice.hall.capacity;
      }
    }

    return result;
  }

  getStatus(): string {
    return this.reservationService.getStatus(this.reservationRequest);
  }

  getOccasionType(): string {
    return this.reservationService.getOccasionType(this.reservationRequest);
  }

  cannotBeJoined(hall: HallModel): boolean {
    if (!this.hallsChoices.some(c => c.choosed === true)) {
      return false;
    } else if (this.hallsChoices.filter(c => c.choosed).some(c => c.hall === hall)) {
      return false;
    } else {
      return !this.hallsChoices.filter(c => c.choosed).every(c => hall.joins.some(h => h.hallId === c.hall.id));
    }
    // return this.hallsChoices.filter(c => c.choosed).every(c => hall.joins.some(h => h.hallId === c.hall.id));
    // return hall.joins.every(j => this.hallsChoices.filter(h => h.choosed).some(h => h.hall.id === j.hallId));
  }
}
