import { Card } from "./card-model";

export interface Collection {
    collectionId: string;
    accountId: string;
    collectionName?: string;
    cards?: Card[];
}