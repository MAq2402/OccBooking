import { Component, OnInit, Input } from '@angular/core';
import { PlaceService } from 'src/app/services/place.service';
import { ActivatedRoute } from '@angular/router';
import { HallService } from 'src/app/services/hall.service';
import { MatDialog } from '@angular/material';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-calendar-section',
  templateUrl: './calendar-section.component.html',
  styleUrls: ['./calendar-section.component.scss']
})
export class CalendarSectionComponent implements OnInit {

  calendar: any;
  dates: Date[];
  placeId: string;
  @Input() disabled = false;
  constructor(private placeService: PlaceService,
              private activatedRoute: ActivatedRoute,
              private hallService: HallService,
              private dialog: MatDialog) { }

  ngOnInit() {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');

    this.calendar = {
      firstDayOfWeek: 0,
      dayNames: ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota', 'Niedziela'],
      dayNamesShort: ['Pon', 'Wto', 'Śro', 'Czw', 'Pią', 'Sob', 'Nie'],
      dayNamesMin: ['Pn', 'Wt', 'Śr', 'Cw', 'Pi', 'So', 'Nd'],
      monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'],
      monthNamesShort: ['Sty', 'Lut', 'Mar', 'Kwie', 'Maj', 'Cze', 'Lip', 'Sie', 'Wrz', 'Paź', 'Lis', 'Gru'],
      today: 'Dzisiaj',
      clear: 'Wyczyść',
      dateFormat: 'mm/dd/yy',
      weekHeader: 'Wk'
    };

    this.getReservedDays();
  }


  saveChanges() {
    this.placeService.getReservedDays(this.placeId).subscribe(result => {
      const datesToSend: Date[] = [];
      for (const date of this.dates.filter(d => !result.includes(d))) {
        datesToSend.push(date);
      }
      this.hallService.makeEmptyPlaceReservations(this.placeId, datesToSend).subscribe(() => this.getReservedDays());
    });
  }

  clearChanges() {
    this.dates = undefined;
    this.getReservedDays();
  }

  private getReservedDays() {
    this.placeService.getReservedDays(this.placeId).subscribe(result => {
      if (result && result.length > 0) {
        this.dates = [];
        for (const date of result) {
          this.dates.push(new Date(date));
        }
      }
    });
  }

  onSelect(value: Date) {
    if (this.disabled) {
      this.getReservedDays();
    }
  }

  onBlur(event: any) {
    // console.log(event);
    // if (!this.disabled) {
    // this.placeService.getReservedDays(this.placeId).subscribe(result => {
    //   if (result && result.length > 0) {
    //     const newDates: Date[] = [];
    //     let maybe;
    //     for (let newDate of result) {
    //       newDate = new Date(newDate);
    //       newDates.push(newDate);
    //       maybe = this.dates.filter(d => d.getDa !== newDate)[0];
    //       //const maybe = this.dates.filter(d => d.getDate() !== newDate.getDate() && d.getMonth() === newDate.getMonth() && d.getFullYear() === newDate.getFullYear())[0];   
    //     }
    //     const dialogRef = this.dialog.open(ReservationDetailsComponent, { data: maybe });
    //     console.log(maybe);
    //     if (maybe) {
    //       const dialogRef = this.dialog.open(ReservationDetailsComponent, { data: maybe });
    //     }
    //     // const date = newDates.filter(item => this.dates.indexOf(item) < 0);
    //     // console.log(date);
    //     // const dialogRef = this.dialog.open(ReservationDetailsComponent, { data: date });
    //     this.getReservedDays();
    //   }
    // });
    // this.getReservedDays();
    // let maybe: Date;
    // console.log(this.dates);
    // for(let value of event) {
    //   if(!maybe) {
    //      //maybe = this.dates.filter(d => d !== value)[0];
    //      maybe = this.dates.filter(d => d.getDate() !== value.getDate() && d.getMonth() === value.getMonth() && d.getFullYear() === value.getFullYear())[0];
    //      console.log(maybe);
    //   }
    // }

    // const dialogRef = this.dialog.open(ReservationDetailsComponent, { data: maybe });
  }
}
