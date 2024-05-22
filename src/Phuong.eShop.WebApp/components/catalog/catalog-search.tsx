import { CatalogBrandModel, CatalogTypeModel } from "@/models/catalog-item";
import { ListFilter } from "lucide-react";
import { Badge } from "../ui/badge";
import CatalogSearchLink from "./catalog-search-link";

type CatalogSearchProps = {
  catalogBrands: CatalogBrandModel[];
  catalogTypes: CatalogTypeModel[];
}

const CatalogSearch = (props: CatalogSearchProps) => {
  const catalogBrands = [
    { id: 0, name: "All" },
    ...props.catalogBrands
  ];
  const catalogTypes = [
    { id: 0, name: "All" },
    ...props.catalogTypes
  ];

  return (
    <div className="flex flex-col gap-6">
      <div className="flex items-center">
        <ListFilter size={32} className="pr-2" />
        Filters
      </div>
      <div>
        <h2 className="text-lg pb-2 border-b border-black font-bold mb-4">Brand</h2>
        <div className="flex flex-wrap gap-1">
          {catalogBrands.map((brand) => (
            <CatalogSearchLink key={brand.id} href="." queryParam={{ name: "brand", value: brand.id.toString() }}>
              <Badge className="cursor-pointer px-2 py-2">{brand.name}</Badge>
            </CatalogSearchLink>
          ))}
        </div>
      </div>

      <div>
        <h2 className="text-lg pb-2 border-b border-black font-bold mb-4">Type</h2>
        <div className="flex flex-wrap gap-1">
          {catalogTypes.map((type) => (
            <CatalogSearchLink key={type.id} href="." queryParam={{ name: "type", value: type.id.toString() }}>
              <Badge key={type.id} className="cursor-pointer px-2 py-2">{type.name}</Badge>
            </CatalogSearchLink>
          ))}
        </div>
      </div>
    </div>
  );
}

export default CatalogSearch;
