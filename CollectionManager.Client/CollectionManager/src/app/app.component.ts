import { Component, OnInit } from '@angular/core';
import { AccountService } from './shared/services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'CollectionManager';
  headerText = 'Biblioplex Collection Manager';
  isLoggedIn = false;

  constructor(protected accountService: AccountService) {
  }

  ngOnInit(): void {
    this.accountService.loggedIn$.subscribe(result => {
      this.isLoggedIn = result;
    })
    
  }

  toggleMenu(){

  }
}

