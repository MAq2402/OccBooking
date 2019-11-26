import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { PlaceService } from 'src/app/services/place.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-make-reservation',
  templateUrl: './make-reservation.component.html',
  styleUrls: ['./make-reservation.component.scss']
})
export class MakeReservationComponent implements OnInit {
  baseInfromationFormGroup: FormGroup;
  addressFormGroup: FormGroup;
  fileFormGroup: FormGroup;
  dateFilter: any;
  reservedDates: Date[] = [];
  placeId: string;

  constructor(private formBuilder: FormBuilder, private placeService: PlaceService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');
    this.placeService.getReservedDays(this.placeId).subscribe(dates => {
      for (const date of dates) {
        this.reservedDates.push(new Date(date));
      }
      console.log(this.reservedDates);
      this.dateFilter = (date: Date) => !this.reservedDates.some(x => x.getFullYear() === date.getFullYear()
      && x.getMonth() === date.getMonth() && x.getDate() === date.getDate());
    });

    this.baseInfromationFormGroup = this.formBuilder.group({
      name: ['', Validators.required],
      hasRooms: ['', Validators.nullValidator],
      description: ['', Validators.nullValidator]
    });

    this.addressFormGroup = this.formBuilder.group({
      city: ['', Validators.required],
      street: ['', Validators.required],
      zipCode: ['', Validators.required],
      province: ['', Validators.required]
    });

    this.fileFormGroup = this.formBuilder.group({
      image: ['', Validators.nullValidator]
    });
  }

  submit() {
    // const model: PlaceModel = {
    //   id: null,
    //   name: this.baseInfromationFormGroup.controls['name'].value,
    //   hasRooms: this.baseInfromationFormGroup.controls['hasRooms'].value,
    //   description: this.baseInfromationFormGroup.controls['description'].value,
    //   street: this.addressFormGroup.controls['street'].value,
    //   city: this.addressFormGroup.controls['city'].value,
    //   zipCode: this.addressFormGroup.controls['zipCode'].value,
    //   province: this.addressFormGroup.controls['province'].value,
    //   additionalOptions: null,
    //   occasionTypes: null,
    //   occasionTypesMaps: null,
    //   image: null,
    //   isConfigured: null
    // };

    // this.placeService.createPlace(this.currentUser.ownerId, model).subscribe(place => {
    //   if (this.fileFormGroup.controls['image'].value) {
    //     this.upload(this.fileFormGroup.controls['image'].value.files[0], place.id);
    //   }
    // });
  }
}
