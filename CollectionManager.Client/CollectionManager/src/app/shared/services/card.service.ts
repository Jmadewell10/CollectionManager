import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL, AUTHENTICATE_ENDPOINT, AUTOCOMPLETE_ENDPOINT, CARD_CONTROLLER } from '../common/apiConstants';

@Injectable({
  providedIn: 'root'
})
export class CardService {

  constructor(private http: HttpClient) { }

  autocomplete(query: string): Observable<string[]> {
    return this.http.get<string[]>(
      API_BASE_URL + CARD_CONTROLLER + AUTOCOMPLETE_ENDPOINT,
      {
        params: {
          q: query
        }
      }
    );
  }
}
