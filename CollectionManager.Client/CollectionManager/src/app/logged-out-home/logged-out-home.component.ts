import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { LoginService } from '../shared/services/login.service';
import { AccountService } from '../shared/services/account.service';
import { ACCOUNT_CREATED_MESSAGE } from '../shared/common/messageConstants';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-logged-out-home',
  templateUrl: './logged-out-home.component.html',
  styleUrl: './logged-out-home.component.scss'
})

export class LoggedOutHomeComponent {
  
  constructor(private dialog: MatDialog,
     protected loginService: LoginService,
     protected accountService: AccountService,
     private snackBar: MatSnackBar
  ) {}

  login(){
    let dialogRef = this.dialog.open(LoginComponent, {
      width: '30%',
      height: '60%'
    })

    dialogRef.afterClosed().subscribe((result) => {
      if(result){
        if(!result.email){
          this.loginService.login(result.userName, result.password)
          .subscribe((loginResuult) => {
            if(loginResuult){
              localStorage.setItem('token', loginResuult);
              this.accountService.loginUser();
            }
            
          });
        }
        if(result.email){
          let request = {
            login: {
              userName: result.userName,
              password: result.password
            },
            firstName: result.firstName,
            lastName: result.lastName,
            email: result.email
          }
          this.accountService.addAccount(request).subscribe((newAccountResponse) => {
            this.snackBar.open(ACCOUNT_CREATED_MESSAGE);
          });
        }
      }
    });

  }

}
