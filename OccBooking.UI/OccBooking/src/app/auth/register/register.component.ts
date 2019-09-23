import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { RegisterModel } from '../models/register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  model: RegisterModel ={
    firstName: '',
    lastName: '',
    password: '',
    confirmPassword: '',
    userName: '',
    email: '',
    phoneNumber: ''
  };

  constructor(private formBuilder: FormBuilder, private service: AuthService, private router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      'firstName': ['', [Validators.required]],
      'lastName':  ['', [Validators.required]],
      'password': ['', [Validators.required]],
      'confirmPassword': ['', [Validators.required]],
      'userName': ['', [Validators.required]],
      'email': ['', [Validators.required]],
      'phoneNumber': ['', [Validators.required]]
    })
  }

  submit() {
    this.model.firstName = this.form.controls['firstName'].value;
    this.model.lastName = this.form.controls['lastName'].value;
    this.model.password = this.form.controls['password'].value;
    this.model.confirmPassword = this.form.controls['confirmPassword'].value;
    this.model.userName = this.form.controls['userName'].value;
    this.model.email = this.form.controls['email'].value;
    this.model.phoneNumber = this.form.controls['phoneNumber'].value;

    this.service.register(this.model).subscribe(() => this.router.navigate(['']));
  }
}
