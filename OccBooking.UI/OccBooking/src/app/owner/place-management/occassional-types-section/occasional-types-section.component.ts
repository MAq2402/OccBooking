import { Component, ViewChild, ElementRef, Output, EventEmitter, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { occasionTypes } from 'src/app/shared/occasionTypes';
import { OccasionTypeMapModel } from 'src/app/models/occasion-type-map';

@Component({
  selector: 'app-occasional-types-section',
  templateUrl: './occasional-types-section.component.html',
  styleUrls: ['./occasional-types-section.component.scss']
})
export class OccasionalTypesSectionComponent {

  @Input() types: OccasionTypeMapModel[] = [];
  visible = true;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  ctrl = new FormControl();
  allTypes = occasionTypes;
  @Output() selected = new EventEmitter<OccasionTypeMapModel>();
  @Output() removed = new EventEmitter<OccasionTypeMapModel>();

  @ViewChild('typeInput', {static: false}) typeInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto', {static: false}) matAutocomplete: MatAutocomplete;

  getNotSelectedTypes(): OccasionTypeMapModel[] {
    if (this.types) {
      return this.allTypes.filter(f => !this.types.includes(f));
    } else {
      return this.allTypes;
    }
  }

  remove(type: OccasionTypeMapModel): void {
    const index = this.types.indexOf(type);

    if (index >= 0) {
      this.types.splice(index, 1);
    }

    this.removed.emit(type);
  }

  onSelected(event: MatAutocompleteSelectedEvent): void {
    this.types.push(event.option.value);
    this.typeInput.nativeElement.value = '';
    this.ctrl.setValue(null);
    this.selected.emit(event.option.value);
  }
}
