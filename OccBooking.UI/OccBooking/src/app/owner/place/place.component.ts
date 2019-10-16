import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PlaceService } from '../services/place.service';
import { PlaceModel } from '../models/place.model';
import { UserModel } from 'src/app/auth/models/user.model';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-place',
  templateUrl: './place.component.html',
  styleUrls: ['./place.component.scss']
})
export class PlaceComponent implements OnInit {
  currentUser: UserModel;
  provinces: string[] = [
    'dolnośląskie',
    'kujawsko-pomorskie',
    'lubelskie',
    'łódzkie',
    'małopolskie',
    'mazowieckie',
    'opolskie',
    'podkarpackie',
    'podlaskie',
    'pomorskie',
    'śląskie',
    'świetokrzyskie',
    'warmińsko-mazurskie',
    'wielkopolskie',
    'zachodniopomorskie'
  ];

  baseInfromationFormGroup: FormGroup;
  addressFormGroup: FormGroup;
  hasRooms = false;

  constructor(private formBuilder: FormBuilder, private placeService: PlaceService, private authService: AuthService) { }

  ngOnInit() {
    this.authService.getCurrentUser().subscribe(user => {
      this.currentUser = user;
    });

    this.baseInfromationFormGroup = this.formBuilder.group({
      name: ['', Validators.required],
      costPerPerson: ['', Validators.required],
      description: ['', Validators.nullValidator]
    });

    this.addressFormGroup = this.formBuilder.group({
      city: ['', Validators.required],
      street: ['', Validators.required],
      zipCode: ['', Validators.required],
      province: ['', Validators.required]
    });
  }

  submit() {
    const model: PlaceModel = {
      name: this.baseInfromationFormGroup.controls['name'].value,
      hasRooms: this.hasRooms,
      costPerPerson: this.baseInfromationFormGroup.controls['costPerPerson'].value,
      description: this.baseInfromationFormGroup.controls['description'].value,
      street: this.addressFormGroup.controls['street'].value,
      city: this.addressFormGroup.controls['city'].value,
      zipCode: this.addressFormGroup.controls['zipCode'].value,
      province: this.addressFormGroup.controls['province'].value
    };

    this.placeService.createPlace(this.currentUser.ownerId, model).subscribe();
  }
}
