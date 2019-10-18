import { Component, OnInit } from '@angular/core';
import { SpinnerService } from './services/spinner.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit {

  isLoading: Subject<boolean> = this.service.isLoading;
  constructor(private service: SpinnerService) { }

  ngOnInit() {
  }

}
