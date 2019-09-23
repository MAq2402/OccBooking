import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginCredentialsModel } from '../models/login-credentials.model';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  model: LoginCredentialsModel = {
    password: '',
    userName: ''
  };

  constructor(private formBuilder: FormBuilder, private service: AuthService, private router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      'password': ['', [Validators.required]],
      'userName': ['', [Validators.required]]
    });
  }

  submit() {
    this.model.password = this.form.controls['password'].value;
    this.model.userName = this.form.controls['userName'].value;

    this.service.login(this.model).subscribe(() => {
      this.router.navigate(['']);
    });
  }
}
