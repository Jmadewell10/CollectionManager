import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  loginForm!: UntypedFormGroup;
  newAccountForm!: UntypedFormGroup;

  constructor(protected builder: UntypedFormBuilder, protected dialogRef: MatDialogRef<LoginComponent>) {
  }


  ngOnInit(): void {

    this.loginForm = this.builder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.newAccountForm = this.builder.group({
      userName: ['', Validators.required],
      password: ['', [Validators.minLength(8), Validators.pattern(/^(?=.*[A-Z])(?=.*[\W_]).{8,}$/)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.email],
    });
    
  }

  onLogin() {
    if (this.loginForm.valid) {
      console.log('Login Data:', this.loginForm.value);
      this.dialogRef.close(this.loginForm.value); // Close dialog and return form data
    }
  }

  onSignUp(){
    if(this.newAccountForm.valid) {
      console.log('new account data:', this.newAccountForm.value);
      this.dialogRef.close(this.newAccountForm.value)
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

  get passwordError() {
    const passwordControl = this.loginForm.get('password');
    if (passwordControl?.hasError('required')) return 'Password is required';
    if (passwordControl?.hasError('minlength')) return 'Password must be at least 8 characters';
    if (passwordControl?.hasError('pattern')) return 'Must include 1 uppercase letter & 1 special character';
    return '';
  }
}
