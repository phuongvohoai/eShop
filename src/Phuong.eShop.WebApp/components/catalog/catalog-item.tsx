"use client"
import { CatalogItemDetailModel, CatalogItemModel } from "@/models/catalog-item";
import { Card, CardContent, CardFooter } from "../ui/card";
import { ShoppingBag } from "lucide-react";
import { Button } from "@/components/ui/button";
import Link from "next/link";
import Image from "next/image";
import { useSnapshot } from "valtio";
import { actions, CatalogItemStoreDetailModel, store } from "../cart/cart-store";
import { useToast } from "@/hooks/use-toast";
import { access } from "fs";

export interface CatalogItemProps extends CatalogItemModel { };

const CatalogItem = (props: CatalogItemStoreDetailModel) => {
  const { toast } = useToast()
  const {itemList} = useSnapshot(store)
  const handleAdd = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    actions.addItem(props)
    toast({
      title: "Added to cart successfully",
      variant: "access"
    });
  }
  return (
    <Link href={`/item/${props.id}`}>
      <Card className="cursor-pointer hover:outline-2 hover:outline-current hover:outline">
      <CardContent className="p-0">
        <Image src={props.pictureUri} alt={props.name} width={600} height={600} className="rounded-t-lg" />
        <div className="flex justify-between text-base p-4 h-20">
          <span className="font-bold">{props.name}</span>
          <span>${props.price}</span>
        </div>
      </CardContent>
      <CardFooter>
        <Button className="w-full" onClick={handleAdd}>
          <ShoppingBag className="mr-2 h-4 w-4"  /> Add to cart
        </Button>
      </CardFooter>
    </Card>
    </Link>
  );
}

export default CatalogItem;
