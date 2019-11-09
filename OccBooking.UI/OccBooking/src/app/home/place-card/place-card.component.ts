import { Component, OnInit, Input } from '@angular/core';
import { PlaceModel } from 'src/app/models/place.model';
declare var require: any;

@Component({
  selector: 'app-place-card',
  templateUrl: './place-card.component.html',
  styleUrls: ['./place-card.component.scss']
})
export class PlaceCardComponent implements OnInit {

  imgUrl = require('../35088_1.jpg');
  @Input() place: PlaceModel;
  constructor() { }

  ngOnInit() {
  }

}
