import { Component, OnInit } from '@angular/core';
import { provinces } from 'src/app/shared/provinces';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SidenavService } from '../services/sidenav.service';
import { PlaceFilterModel } from '../models/place-filter.model';
import { occasionTypes } from 'src/app/shared/occasionTypes';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  formGroup: FormGroup;
  provinces = provinces;
  occasions = occasionTypes;
  constructor(private formBuilder: FormBuilder, private sidenavService: SidenavService) { }

  ngOnInit() {
    this.initFromGroup();
  }

  filter() {
    const model: PlaceFilterModel = {
      name: this.formGroup.controls.name.value,
      province: this.formGroup.controls.province.value,
      city: this.formGroup.controls.city.value,
      minCostPerPerson: this.formGroup.controls.minCostPerPerson.value,
      maxCostPerPerson: this.formGroup.controls.maxCostPerPerson.value,
      minCapacity: this.formGroup.controls.minCapacity.value,
      occasionType: this.formGroup.controls.occasion.value,
      freeFrom: this.formGroup.controls.freeFrom.value,
      freeTo: this.formGroup.controls.freeTo.value
    };
    this.sidenavService.announceFiltering(model);
  }

  clear() {
    this.formGroup.reset();
    this.initFromGroup();
  }

  private initFromGroup() {
    this.formGroup = this.formBuilder.group({
      name: ['', Validators.nullValidator],
      province: ['', Validators.nullValidator],
      city: ['', Validators.nullValidator],
      minCostPerPerson: ['', Validators.nullValidator],
      maxCostPerPerson: ['', Validators.nullValidator],
      minCapacity: ['', Validators.nullValidator],
      occasion: ['', Validators.nullValidator],
      freeFrom: ['', Validators.nullValidator],
      freeTo: ['', Validators.nullValidator]
    });
  }
}
