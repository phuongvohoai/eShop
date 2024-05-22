import { CatalogItemModel } from "@/models/catalog-item";
import CatalogItem from "./catalog-item";
import { SmartPagination } from "../smart-pagination";

type CatalogItemListProps = {
  currentPage: number;
  totalPage: number;
  catalogItems: CatalogItemModel[];
};

const CatalogItemList = (props: CatalogItemListProps) => {
  return (
    <div className="flex flex-col gap-10">
      <div className="grid grid-cols-3 gap-5">
        {props.catalogItems.map((item) => (
          <CatalogItem key={item.id} {...item} />
        ))}
      </div>
      <SmartPagination totalPage={props.totalPage} currentPage={props.currentPage} />
    </div>
  );
}

export default CatalogItemList;
