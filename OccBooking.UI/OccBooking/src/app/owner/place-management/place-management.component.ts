import { Component, OnInit, Directive, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlaceModel } from '../../models/place.model';
import { CreateMenuDialogComponent } from './create-menu-dialog/create-menu-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { PlaceService } from 'src/app/services/place.service';
import { MenuService } from 'src/app/services/menu.service';
import { IngredientModel } from 'src/app/models/ingredient.model';
import { MenuModel } from 'src/app/models/menu.model';
import { CreateOptionDialogComponent } from './create-option-dialog/create-option-dialog.component';
import { OccasionTypeMapModel } from 'src/app/models/occasion-type-map';
import { occasionTypes } from 'src/app/shared/occasionTypes';
import { HallService } from 'src/app/services/hall.service';
import { HallModel } from 'src/app/models/hall.model';
import { CreateHallDialogComponent } from './create-hall-dialog/create-hall-dialog.component';
import { MatTableDataSource, MatPaginator } from '@angular/material';
declare var require: any;

@Component({
  selector: 'app-place-management',
  templateUrl: './place-management.component.html',
  styleUrls: ['./place-management.component.scss']
})
export class PlaceManagementComponent implements OnInit {

  place: PlaceModel;
  halls: HallModel[];
  placeId: string;

  constructor(private activatedRoute: ActivatedRoute,
              private placeService: PlaceService,
              public dialog: MatDialog,
              private hallService: HallService) { }

  ngOnInit() {
    this.placeId = this.activatedRoute.snapshot.paramMap.get('id');

    this.getPlace();
    this.getHalls();
  }

  createAdditionalOption() {
    const dialogRef = this.dialog.open(CreateOptionDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.placeService.addOption(this.placeId, result).subscribe(() => this.getPlace());
      }
    });
  }

  private getPlace() {
    this.placeService.getPlace(this.placeId).subscribe(place => {
      this.place = place;
      if (this.place.image) {
        this.place.image = 'data:image/png;base64,' + place.image;
      } else {
        this.place.image = require('../../../assets/default-image.jpg');
      }

      this.place.occasionTypesMaps = this.placeService.mapToOccasionTypeMap(this.place.occasionTypes);
    });
  }

  occasionTypeAdded(occasionType: OccasionTypeMapModel) {
    this.placeService.allowOccasionType(this.placeId, occasionType.value).subscribe(() => this.getPlace());
  }

  occasionTypeRemoved(occasionType: OccasionTypeMapModel) {
    this.placeService.disallowOccasionType(this.placeId, occasionType.value).subscribe(() => this.getPlace());
  }

  createHall() {
    const dialogRef = this.dialog.open(CreateHallDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.hallService.createHall(this.placeId, result).subscribe(() => this.getHalls());
      }
    });
  }

  private getHalls() {
    this.hallService.getHalls(this.placeId).subscribe(halls => this.halls = halls);
  }
}
