import { CatalogItemModel } from "@/models/catalog-item";
import { Card, CardContent, CardFooter } from "../ui/card";

export interface CatalogItemProps extends CatalogItemModel { };

const CatalogItem = (props: CatalogItemProps) => {
  return (
    <Card className="cursor-pointer hover:outline-2 hover:outline-current hover:outline">
      <CardContent className="p-0">
        <img src={props.pictureUri} alt={props.name} width={600} height={600} className="rounded-t-lg" />
      </CardContent>
      <CardFooter className="flex justify-between text-base pt-4">
        <span className="font-bold">{props.name}</span>
        <span>${props.price}</span>
      </CardFooter>
    </Card>
  );
}

export default CatalogItem;
