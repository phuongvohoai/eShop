"use client";
import { Card, CardContent } from "@/components/ui/card";
import { CircleX, Clock } from "lucide-react";
import Link from "next/link";
import QuantitySelector from "./quantity-selector";
import Image from "next/image";
import { CatalogItemCartDetailModel, removeItemFromCart } from "./cart-store";

export default function CartItem(props: CatalogItemCartDetailModel) {
  const handleDelete = () => {
    removeItemFromCart(props.id)
  }
  return (
    <Card>
      <CardContent className="p-3 relative">
        <div className="absolute top-1 right-1 cursor-pointer">
          <CircleX color="#fc0303" size={16} onClick={handleDelete} />
        </div>
        <div className="flex gap-4">
          <div className="w-30 shrink-0">
            <Link href={`/catalog/${props.id}`}>
              <Image
                src={props.pictureUri}
                alt={props.name}
                width={120}
                height={120}
                className="object-cover object-center"
              />
            </Link>
          </div>
          <div className="flex flex-1 flex-col justify-around text-lg">
            <p className="flex justify-between w-full">
              <span className="uppercase">{props.name}</span>
              <span >${(props.price * props.quantity).toFixed(1)}</span>
            </p>
            <p className="text-sm flex gap-1 items-center text-red-400">
              <Clock size={14}></Clock>
              {props.availableStock} in stock
            </p>
            <QuantitySelector quantity={props.quantity} maxQuantity={props.availableStock} id={props.id} />
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
