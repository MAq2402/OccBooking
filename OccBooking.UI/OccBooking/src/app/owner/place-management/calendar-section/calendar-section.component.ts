import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-calendar-section',
  templateUrl: './calendar-section.component.html',
  styleUrls: ['./calendar-section.component.scss']
})
export class CalendarSectionComponent implements OnInit {

  calendar: any;
  constructor() { }

  ngOnInit() {
    this.calendar = {
      firstDayOfWeek: 0,
      dayNames: ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"],
      dayNamesShort: ["Pon", "Wto", "Śro", "Czw", "Pią", "Sob", "Nie"],
      dayNamesMin: ["Pn", "Wt", "Śr", "Cw", "Pi", "So", "Nd"],
      monthNames: ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"],
      monthNamesShort: ["Sty", "Lut", "Mar", "Kwie", "Maj", "Cze", "Lip", "Sie", "Wrz", "Paź", "Lis", "Gru"],
      today: 'Dzisiaj',
      clear: 'Wyczyść',
      dateFormat: 'mm/dd/yy',
      weekHeader: 'Wk'
    };
  }

  saveChanges() {

  }

  clearChanges() {

  }
}
