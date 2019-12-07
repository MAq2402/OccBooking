import { Component, OnInit, Input } from '@angular/core';
import { PlaceService } from 'src/app/services/place.service';
import { ActivatedRoute } from '@angular/router';
import { HallService } from 'src/app/services/hall.service';
import { MatDialog } from '@angular/material';

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

    this.activatedRoute.paramMap.subscribe(params => {
      this.placeId = params.get('id');
      this.refreshReservedDays();
    });
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

  refreshReservedDays() {
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

  onSelect(event: Date[]) {
    if (this.disabled) {
      this.getReservedDays();
    } else {
      console.log(this.dates);
      console.log(event);
      let unselectedDate: Date;
      if (this.dates.length > event.length) {
             for (let date of this.dates) {
            if (event.every(d => d !== date)) {
              unselectedDate = date;
              break;
            }
          }
      }
      console.log(unselectedDate);
    }
  }
}
