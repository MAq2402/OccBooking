import { Component, OnInit } from '@angular/core';
import { provinces } from 'src/app/shared/provinces';
import { occassions } from 'src/app/shared/occasions';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PlaceService } from 'src/app/owner/services/place.service';
import { SidenavService } from '../services/sidenav.service';
import { PlaceFilterModel } from '../models/place-filter.model';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  formGroup: FormGroup;
  provinces = provinces;
  occassions = occassions;
  constructor(private formBuilder: FormBuilder, private sidenavService: SidenavService) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      name: ['', Validators.nullValidator],
      province: ['', Validators.nullValidator],
      city: ['', Validators.nullValidator],
      minCostPerPerson: ['', Validators.nullValidator],
      maxCostPerPerson: ['', Validators.nullValidator],
      minCapacity: ['', Validators.nullValidator],
      occassion: ['', Validators.nullValidator]
    });
  }

  filter() {
    const model: PlaceFilterModel = {
      name: this.formGroup.controls.name.value,
      province: this.formGroup.controls.province.value,
      city: this.formGroup.controls.city.value,
      minCostPerPerson: this.formGroup.controls.minCostPerPerson.value,
      maxCostPerPerson: this.formGroup.controls.maxCostPerPerson.value,
      minCapacity: this.formGroup.controls.minCapacity.value,
      occassionType: this.formGroup.controls.occassion.value,
    };
    this.sidenavService.announceFiltering(model);
  }
}
