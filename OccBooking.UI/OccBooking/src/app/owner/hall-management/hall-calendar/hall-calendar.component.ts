import { Component, OnInit, Input } from '@angular/core';
import { PlaceService } from 'src/app/services/place.service';
import { ActivatedRoute } from '@angular/router';
import { HallService } from 'src/app/services/hall.service';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-hall-calendar',
  templateUrl: './hall-calendar.component.html',
  styleUrls: ['./hall-calendar.component.scss']
})
export class HallCalendarComponent implements OnInit {

  calendar: any;
  dates: Date[];
  hallId: string;
  @Input() disabled = false;
  constructor(private activatedRoute: ActivatedRoute,
              private hallService: HallService,
              private dialog: MatDialog) { }

  ngOnInit() {
    this.hallId = this.activatedRoute.snapshot.paramMap.get('id');

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
    this.hallService.getReservedDays(this.hallId).subscribe(result => {
      const datesToSend: Date[] = [];
      for (const date of this.dates.filter(d => !result.includes(d))) {
        datesToSend.push(date);
      }
      this.hallService.makeEmptyHallReservations(this.hallId, datesToSend).subscribe(() => this.getReservedDays());
    });
  }

  clearChanges() {
    this.dates = undefined;
    this.getReservedDays();
  }

  private getReservedDays() {
    this.hallService.getReservedDays(this.hallId).subscribe(result => {
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

}
