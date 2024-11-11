"use client";

import { Card, CardContent } from "@/components/ui/card";
import { CircleX, Clock, Minus, Plus } from "lucide-react";
import Link from "next/link";
import { Input } from "@/components/ui/input";
import { useState } from "react";
import { cn } from "@/lib/utils";
import QuantitySelector from "./quantity-selector";
import Image from "next/image";
import { CatalogItemDetailModel } from "@/models/catalog-item";
import { useSnapshot } from "valtio";
import { actions, CatalogItemStoreDetailModel, store } from "./cart-store";

export default function CartItem(props: CatalogItemStoreDetailModel) {
  const [number, setNumber] = useState<number>(props.quantity);
  const { itemList } = useSnapshot(store)
  const catalogItem: CatalogItemStoreDetailModel = {
    id: props.id,
    name: props.name,
    price: props.price,
    availableStock: props.availableStock,
    catalogBrand: props.catalogBrand,
    catalogType: props.catalogType,
    description: props.description,
    pictureUri: props.pictureUri,
    quantity: props.quantity,
  };
  const handleDelete = () =>{
    actions.removeTodo(catalogItem.id)
  }
  return (
    <Card>
      <CardContent className="p-3 relative">
        <div className="absolute top-1 right-1 cursor-pointer">
          <CircleX color="#fc0303" size={16} onClick={handleDelete} />
        </div>
        <div className="flex gap-4">
          <div className="w-30 shrink-0">
            <Link href={`/catalog/${catalogItem.id}`}>
              <Image
                src={catalogItem.pictureUri}
                alt={catalogItem.name}
                width={120}
                height={120}
                className="object-cover object-center"
              />
            </Link>
          </div>
          <div className="flex flex-1 flex-col justify-around text-lg">
            <p className="flex justify-between w-full">
              <span className="uppercase">{catalogItem.name}</span>
              <span >${(catalogItem.price * number).toFixed(1)}</span>
            </p>
            <p className="text-sm flex gap-1 items-center text-red-400">
              <Clock size={14}></Clock>
              {catalogItem.availableStock} in stock
            </p>

            <QuantitySelector quantity={number} onChange={setNumber} maxQuantity={catalogItem.availableStock} id={catalogItem.id} />
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
