import { CatalogItemModel } from "@/models/catalog-item";
import CatalogItem from "./catalog-item";

type CatalogItemListProps = {
  catalogItems: CatalogItemModel[];
};

const CatalogItemList = (props: CatalogItemListProps) => {
  return (
    <div className="grid grid-cols-3 gap-5">
      {props.catalogItems.map((item) => (
        <CatalogItem key={item.id} {...item} />
      ))}
    </div>
  );
}

export default CatalogItemList;
