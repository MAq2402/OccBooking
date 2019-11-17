import { Component, OnInit } from '@angular/core';
import { PlaceService } from 'src/app/services/place.service';
import { ActivatedRoute } from '@angular/router';
import { HallService } from 'src/app/services/hall.service';

@Component({
  selector: 'app-calendar-section',
  templateUrl: './calendar-section.component.html',
  styleUrls: ['./calendar-section.component.scss']
})
export class CalendarSectionComponent implements OnInit {

  calendar: any;
  dates: Date[];
  placeId: string;
  constructor(private placeService: PlaceService, private activatedRoute: ActivatedRoute, private hallService: HallService) { }

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
      for (const date of  this.dates.filter(d => !result.includes(d))) {
        datesToSend.push(date);
      }
      this.hallService.makeEmptyReservations(this.placeId, datesToSend).subscribe(() => this.getReservedDays());
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
}
