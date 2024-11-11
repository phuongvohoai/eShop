import { CatalogItemDetailModel } from "@/models/catalog-item";
import { proxy, useSnapshot } from "valtio";

export interface CatalogItemStoreDetailModel extends CatalogItemDetailModel {
    quantity: number,
}

export const store = proxy({
    itemList: [] as CatalogItemStoreDetailModel[],
});
export const actions = {
    // add item to array
    addItem: (item: CatalogItemStoreDetailModel) => {
        const existingItem = store.itemList.find((i) => item.id === i.id);
        if (existingItem) {
            existingItem.quantity += 1;
        } else {
            store.itemList.push({ ...item, quantity: 1 });
        }
    },
    //remove item
    removeTodo: (id: number) => {
        store.itemList = store.itemList.filter((item) => item.id !== id)
    },
    incrementQuantity: (id: number) => {
        const findItem = store.itemList.find((item) => item.id === id)
        if (findItem) {
            findItem.quantity += 1;
        }
        else return;
    }
}