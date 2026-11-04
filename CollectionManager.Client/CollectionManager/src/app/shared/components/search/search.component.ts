import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, filter, Observable, switchMap } from 'rxjs';
import { CardService } from '../../services/card.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent implements OnInit {

  searchFormControl = new FormControl('');
  options$!: Observable<string[]>;
  
  constructor(private cardService: CardService){}

  ngOnInit(): void {    
  this.options$ = this.searchFormControl.valueChanges.pipe(
    debounceTime(250),
    distinctUntilChanged(),
    filter(text => text!.length >= 2),
    switchMap(text => this.cardService.autocomplete(text!))
  );
  }

}
