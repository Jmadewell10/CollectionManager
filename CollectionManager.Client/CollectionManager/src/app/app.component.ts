import { Component, OnInit, ViewChild } from '@angular/core';
import { AccountService } from './shared/services/account.service';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  @ViewChild('sidenav') sidenav!: MatSidenav
  title = 'CollectionManager';
  headerText = 'Biblioplex Collection Manager';
  isLoggedIn = false;


  constructor(protected accountService: AccountService, protected router: Router) {
  }

  ngOnInit(): void {
    this.accountService.loggedIn$.subscribe(result => {
      this.isLoggedIn = result;
    });

    this.accountService.checkToken().subscribe(isValid => {
      if(isValid){
        this.router.navigate(['/home']);
      }
    });
  }

  toggleMenu(){
    this.sidenav.toggle();
  }
}

