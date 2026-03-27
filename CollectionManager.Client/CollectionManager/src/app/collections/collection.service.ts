import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NewCollectionDto } from '../shared/models/dto/new-collection-dto';
import { ADD_COLLECTION_ENDPOINT, API_BASE_URL, COLLECTION_CONTROLLER } from '../shared/common/apiConstants';

@Injectable({
  providedIn: 'root'
})
export class CollectionService {

  constructor(private http: HttpClient) { }

  public addCollection(newCollection: NewCollectionDto) : Observable<any>{
    return this.http.post(API_BASE_URL + COLLECTION_CONTROLLER + ADD_COLLECTION_ENDPOINT, newCollection);
  }
}
