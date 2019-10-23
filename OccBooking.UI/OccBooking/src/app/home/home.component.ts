import { Component, OnInit } from '@angular/core';
declare var require: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  imgUrl = require('35088_1.jpg');
  constructor() { }

  ngOnInit() {
  }

}
