import { Component, OnInit, Input } from '@angular/core';
import { PlaceModel } from 'src/app/models/place.model';
declare var require: any;

@Component({
  selector: 'app-place-card',
  templateUrl: './place-card.component.html',
  styleUrls: ['./place-card.component.scss']
})
export class PlaceCardComponent implements OnInit {

  imgUrl: any;
  @Input() place: PlaceModel;
  constructor() { }

  ngOnInit() {
    if (this.place.image) {
      this.imgUrl = 'data:image/png;base64,' + this.place.image;
    } else {
      this.imgUrl = require('../default-image.jpg');
    }
  }

}
