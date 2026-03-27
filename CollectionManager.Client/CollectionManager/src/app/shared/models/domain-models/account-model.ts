import { Collection } from "./collection-model";

export interface Account {
    accountId: string;
    userId: string;
    keyId: string;
    userName?: string;
    password?: string;
    collections?: Collection[];
}