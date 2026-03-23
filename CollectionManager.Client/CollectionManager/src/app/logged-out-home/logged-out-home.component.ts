import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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

  constructor(
    private dialog: MatDialog,
    protected loginService: LoginService,
    protected accountService: AccountService,
    private snackBar: MatSnackBar
  ) {}

  login() {
    const dialogRef = this.dialog.open(LoginComponent, {
      width: '400px',
      autoFocus: true,
      panelClass: 'dark-dialog'
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (!result) return;

      if (!result.email) {
        this.loginService.login(result.userName, result.password)
          .subscribe((loginResult) => {
            if (loginResult) {
              localStorage.setItem('token', loginResult);
              this.accountService.loginUser();
            }
          });
      } else {
        const request = {
          login: {
            userName: result.userName,
            password: result.password
          },
          firstName: result.firstName,
          lastName: result.lastName,
          email: result.email
        };

        this.accountService.addAccount(request).subscribe(() => {
          this.snackBar.open(ACCOUNT_CREATED_MESSAGE, 'Dismiss', {
            duration: 4000,
            panelClass: 'dark-snackbar'
          });
        });
      }
    });
  }
}