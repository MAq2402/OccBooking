import { Component, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { EnumType } from 'src/app/shared/enum-type';
import { occasionTypes } from 'src/app/shared/occasionTypes';

@Component({
  selector: 'app-occasional-types-section',
  templateUrl: './occasional-types-section.component.html',
  styleUrls: ['./occasional-types-section.component.scss']
})
export class OccasionalTypesSectionComponent {

  // @Input() occasionalTypes: Occas
  visible = true;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  ctrl = new FormControl();
  types: EnumType[] = [];
  allTypes = occasionTypes;
  @Output() selected = new EventEmitter<EnumType>();
  @Output() removed = new EventEmitter<EnumType>();

  @ViewChild('typeInput', {static: false}) typeInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto', {static: false}) matAutocomplete: MatAutocomplete;

  getNotSelectedTypes(): EnumType[] {
    return this.allTypes.filter(f => !this.types.includes(f));
  }

  remove(type: EnumType): void {
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
