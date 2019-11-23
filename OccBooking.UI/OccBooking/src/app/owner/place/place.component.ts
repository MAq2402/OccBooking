import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PlaceModel } from '../../models/place.model';
import { UserModel } from 'src/app/auth/models/user.model';
import { AuthService } from 'src/app/auth/services/auth.service';
import { provinces } from 'src/app/shared/provinces';
import { PlaceService } from 'src/app/services/place.service';

@Component({
  selector: 'app-place',
  templateUrl: './place.component.html',
  styleUrls: ['./place.component.scss']
})
export class PlaceComponent implements OnInit {
  currentUser: UserModel;
  provinces = provinces;

  baseInfromationFormGroup: FormGroup;
  addressFormGroup: FormGroup;
  fileFormGroup: FormGroup;
  hasRooms = false;

  constructor(private formBuilder: FormBuilder,
              private placeService: PlaceService,
              private authService: AuthService) { }

  ngOnInit() {
    this.authService.getCurrentUser().subscribe(user => {
      this.currentUser = user;
    });

    this.baseInfromationFormGroup = this.formBuilder.group({
      name: ['', Validators.required],
      hasRooms: ['', Validators.nullValidator],
      costPerPerson: ['', Validators.required],
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
    const model: PlaceModel = {
      id: null,
      name: this.baseInfromationFormGroup.controls['name'].value,
      hasRooms: this.baseInfromationFormGroup.controls['hasRooms'].value,
      costPerPerson: this.baseInfromationFormGroup.controls['costPerPerson'].value,
      description: this.baseInfromationFormGroup.controls['description'].value,
      street: this.addressFormGroup.controls['street'].value,
      city: this.addressFormGroup.controls['city'].value,
      zipCode: this.addressFormGroup.controls['zipCode'].value,
      province: this.addressFormGroup.controls['province'].value,
      additionalOptions: null,
      occasionTypes: null,
      occasionTypesMaps: null,
      image: null
    };

    this.placeService.createPlace(this.currentUser.ownerId, model).subscribe(place => {
      this.upload(this.fileFormGroup.controls['image'].value.files[0], place.id);
    });
  }

   private upload(file, placeId: string) {
    if (file) {
      const formData = new FormData();
      formData.append(file.name, file);

      this.placeService.uploadFile(placeId, formData);
    }
  }
}
