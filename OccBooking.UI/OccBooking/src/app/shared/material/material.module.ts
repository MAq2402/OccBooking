import { NgModule } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatStepperModule} from '@angular/material/stepper';
import {MatSelectModule} from '@angular/material/select';
import {MatCheckboxModule} from '@angular/material/checkbox';

@NgModule({
  declarations: [],
  imports: [
      MatButtonModule,
      MatInputModule,
      MatFormFieldModule,
      MatCardModule,
      MatIconModule,
      MatMenuModule,
      MatStepperModule,
      MatSelectModule,
      MatCheckboxModule
  ],
  exports: [
      MatButtonModule,
      MatInputModule,
      MatFormFieldModule,
      MatCardModule,
      MatIconModule,
      MatMenuModule,
      MatStepperModule,
      MatSelectModule,
      MatCheckboxModule
  ]
})
export class MaterialModule { }
