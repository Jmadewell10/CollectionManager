import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Collection } from '../../shared/models/domain-models/collection-model';
import { NewCollectionDto } from '../../shared/models/dto/new-collection-dto';

export interface AddEditCollectionDialogData {
    collection?: Collection;
}

@Component({
    selector: 'app-add-edit-collection',
    templateUrl: './add-edit-collection.component.html',
    styleUrl: './add-edit-collection.component.scss'
})
export class AddEditCollectionComponent implements OnInit {

    collectionForm!: FormGroup;
    isEditMode = false;

    constructor(
        private fb: FormBuilder,
        private dialogRef: MatDialogRef<AddEditCollectionComponent>,
        @Inject(MAT_DIALOG_DATA) public data: AddEditCollectionDialogData
    ) {}

    ngOnInit(): void {
        this.isEditMode = !!this.data?.collection;

        this.collectionForm = this.fb.group({
            collectionName: [
                this.data?.collection?.collectionName ?? '',
                [Validators.required, Validators.maxLength(100)]
            ]
        });
    }

    save(): void {
        if (this.collectionForm.invalid) return;

        const result: NewCollectionDto = {
            ...(this.isEditMode && { collectionId: this.data.collection!.collectionId }),
            collectionName: this.collectionForm.value.collectionName
        };

        this.dialogRef.close(result);
    }

    cancel(): void {
        this.dialogRef.close(null);
    }
}