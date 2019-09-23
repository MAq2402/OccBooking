import { NgModule } from '@angular/core';
import { RegisterComponent } from './register/register.component';
import { SharedModule } from '../shared/shared.module';
import { LoginComponent } from './login/login.component';



@NgModule({
  declarations: [RegisterComponent, LoginComponent],
  imports: [
    SharedModule
  ],
  exports: [
    RegisterComponent
  ]
})
export class AuthModule { }
