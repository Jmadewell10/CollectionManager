import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../../../login/login.component';
import { LoginService } from '../../services/login.service';
import { AccountService } from '../../services/account.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ACCOUNT_CREATED_MESSAGE } from '../../common/messageConstants';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  @Input() headerText: string = "";
  imageUrl = 'assets/images/logo.png';


  constructor(protected dialog: MatDialog, private loginService: LoginService,
     private accountService: AccountService, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    
  }

  openLoginModal(){
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
