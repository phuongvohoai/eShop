import { Card, CardContent, CardFooter } from "../ui/card";
import Link from "next/link";
import Image from "next/image";
import {CatalogItemCartDetailModel} from "../cart/cart-store";
import AddToCartButton from "./add-to-cart-button";

const CatalogItem = (props: CatalogItemCartDetailModel) => {
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
        <AddToCartButton {...props}/>
      </CardFooter>
    </Card>
    </Link>
  );
}

export default CatalogItem;
