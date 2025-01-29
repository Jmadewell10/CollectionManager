import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../../../login/login.component';
import { LoginService } from '../../services/login.service';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  @Input() headerText: string = "";
  imageUrl = 'assets/images/logo.png';


  constructor(protected dialog: MatDialog, private loginService: LoginService,
     private accountService: AccountService) {}

  ngOnInit(): void {
    
  }

  openLoginModal(){
    let dialogRef = this.dialog.open(LoginComponent, {
      width: '30%',
      height: '60%'
    })

    dialogRef.afterClosed().subscribe();

  }
}
