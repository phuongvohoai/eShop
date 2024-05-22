import { CatalogBrandModel, CatalogTypeModel } from "@/models/catalog-item";
import { ListFilter } from "lucide-react";
import { Badge } from "../ui/badge";

type CatalogSearchProps = {
  catalogBrands: CatalogBrandModel[];
  catalogTypes: CatalogTypeModel[];
}

const CatalogSearch = (props: CatalogSearchProps) => {
  return (
    <div className="flex flex-col gap-6">
      <div className="flex items-center">
        <ListFilter size={32} className="pr-2" />
        Filters
      </div>
      <div>
        <h2 className="text-lg pb-2 border-b border-black font-bold mb-4">Brand</h2>
        <div className="flex flex-wrap gap-1">
          {props.catalogBrands.map((brand) => (
            <Badge key={brand.id} className="cursor-pointer px-2 py-2">{brand.name}</Badge>
          ))}
        </div>
      </div>

      <div>
        <h2 className="text-lg pb-2 border-b border-black font-bold mb-4">Type</h2>
        <div className="flex flex-wrap gap-1">
          {props.catalogTypes.map((type) => (
            <Badge key={type.id} className="cursor-pointer px-2 py-2">{type.name}</Badge>
          ))}
        </div>
      </div>
    </div>
  );
}

export default CatalogSearch;
