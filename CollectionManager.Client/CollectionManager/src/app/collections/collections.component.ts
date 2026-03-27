import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Collection } from '../shared/models/collection-model';

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrl: './collections.component.scss'
})
export class CollectionsComponent {

  collections: Collection[] = [];

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
    // wire up your collections service here
  }

  openAddCollection(): void {
    // open your add collection dialog here
  }

  openCollection(collection: Collection): void {
    // navigate into the collection here
  }

}
