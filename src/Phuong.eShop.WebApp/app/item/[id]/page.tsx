import { Button } from "@/components/ui/button";
import CatalogApi from "@/lib/api/catalog-api";
import { Clock, ShoppingBag } from "lucide-react";

interface CatalogItemProps {
  params: {
    id: string;
  }
}

export default async function CatalogItem({ params }: CatalogItemProps) {
  const catalogItem = await CatalogApi.getCatalogItem(+params.id);
  return (
    <div className="flex py-20">
      <div className="flex-1">
        <img src={catalogItem.pictureUri} alt={catalogItem.name} />
      </div>
      <div className="flex flex-col gap-4 w-1/3">
        <h1 className="text-5xl text-muted-foreground">{catalogItem.name}</h1>
        <p className="text-xl font-medium">$ {catalogItem.price}</p>
        <p className="text-lg flex gap-2 items-center text-red-400"><Clock size={18}></Clock>{catalogItem.availableStock} in stock</p>
        <p className="text-lg">Brand: <b>{catalogItem.catalogBrand}</b></p>
        <p className="text-lg">Type: <b>{catalogItem.catalogType}</b></p>
        <p className="text-justify">{catalogItem.description}</p>
        <Button className="w-full">
          <ShoppingBag className="mr-2 h-4 w-4" /> Add to cart
        </Button>
      </div>
    </div >
  )
}
