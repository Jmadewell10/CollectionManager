import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Collection } from '../shared/models/domain-models/collection-model';
import { AddEditCollectionComponent } from './add-edit-collection/add-edit-collection.component';
import { CollectionService } from './collection.service';
import { NewCollectionDto } from '../shared/models/dto/new-collection-dto';
import { ViewCollectionComponent } from './view-collection/view-collection.component';

@Component({
    selector: 'app-collections',
    templateUrl: './collections.component.html',
    styleUrl: './collections.component.scss'
})
export class CollectionsComponent {

    collections: Collection[] = [];

    constructor(private dialog: MatDialog, private collectionService: CollectionService) { }

    ngOnInit(): void {
        this.loadCollections();
    }


    openAddCollection(): void {
        const dialogRef = this.dialog.open(AddEditCollectionComponent, {
            width: '420px',
            autoFocus: true,
            panelClass: 'dark-dialog',
            data: {}
        });

        dialogRef.afterClosed().subscribe((result: NewCollectionDto) => {
            if (result) {
                this.collectionService.addCollection(result).subscribe({
                    next: (newCollection: Collection) => this.collections = [...this.collections, newCollection],
                    error: (err)=> console.error('Failed to add collection', err)
                });
            }
        });
    }

    openEditCollection(collection: Collection): void {
        const dialogRef = this.dialog.open(AddEditCollectionComponent, {
            width: '420px',
            autoFocus: true,
            panelClass: 'dark-dialog',
            data: { collection }
        });

        dialogRef.afterClosed().subscribe((result: Partial<Collection>) => {
            if (result) {
                // call your collection service here
            }
        });
    }

    openCollection(collection: Collection): void {
        const dialogRef = this.dialog.open(ViewCollectionComponent, {
            width: '420px',
            autoFocus: true,
            panelClass: 'dark-dialog',
            data: {}
        });
    }

    private loadCollections(): void {
    this.collectionService.getAllCollectionsForUser().subscribe({
        next: (result: Collection[]) => this.collections = result,
        error: (err) => console.error('Failed to load collections', err)
    });
}

}
