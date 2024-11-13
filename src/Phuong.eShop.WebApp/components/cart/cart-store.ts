import { CatalogItemDetailModel } from "@/models/catalog-item";
import { proxy, useSnapshot } from "valtio";

export interface CatalogItemCartDetailModel extends CatalogItemDetailModel {
    quantity: number,
}

export const cartStore = proxy({
    itemList: [] as CatalogItemCartDetailModel[],
});
export const addItemIntoCart = (item: CatalogItemCartDetailModel) => {
    const existingItem = cartStore.itemList.find((i) => item.id === i.id);
    if (existingItem) {
        existingItem.quantity += 1;
    } else {
        cartStore.itemList.push({ ...item, quantity: 1 });
    }
  }
  
export const removeItemFromCart = (id: number) => {
    cartStore.itemList = cartStore.itemList.filter((item) => item.id !== id)
}
export const incrementItemQuantity = (id: number) => {
    const findItem = cartStore.itemList.find((item) => item.id === id)
    if (findItem) {
        findItem.quantity += 1;
    }
    else return;
}
export const decrementItemQuantity = (id: number) => {
    const findItem = cartStore.itemList.find((item) => item.id === id)
    if (findItem) {
        findItem.quantity -= 1;
    }
    else return;
}

export const useCartStore = () => useSnapshot(cartStore);
