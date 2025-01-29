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

  constructor(protected builder: UntypedFormBuilder, protected dialogRef: MatDialogRef<LoginComponent>) {
  }


  ngOnInit(): void {

    this.loginForm = this.builder.group({
      userName: ['', Validators.required],
      password: ['', [Validators.minLength(8), Validators.pattern(/^(?=.*[A-Z])(?=.*[\W_]).{8,}$/)]]
    });
    
  }

  onLogin() {
    if (this.loginForm.valid) {
      console.log('Login Data:', this.loginForm.value);
      this.dialogRef.close(this.loginForm.value); // Close dialog and return form data
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
