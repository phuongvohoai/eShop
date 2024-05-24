"use client";

import { Card, CardContent } from "@/components/ui/card";
import { Clock, Minus, Plus } from "lucide-react";
import Link from "next/link";
import { Input } from "@/components/ui/input";
import { useState } from "react";
import { cn } from "@/lib/utils";
import QuantitySelector from "./quantity-selector";
import Image from "next/image";

export default function CartItem() {
  const [number, setNumber] = useState<number>(1);
  const catalogItem = {
    id: 1,
    name: "Test Item",
    price: 10,
    availableStock: 10,
    catalogBrand: "Test Brand",
    catalogType: "Test Type",
    description: "Test Description",
    pictureUri: "http://localhost:50211/api/catalog/items/1/pic",
  };

  return (
    <Card>
      <CardContent className="p-3">
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
              <span>$ {catalogItem.price}</span>
            </p>
            <p className="text-sm flex gap-1 items-center text-red-400">
              <Clock size={14}></Clock>
              {catalogItem.availableStock} in stock
            </p>

            <QuantitySelector quantity={number} onChange={setNumber} maxQuantity={catalogItem.availableStock} />
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
