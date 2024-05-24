import {
  CatalogBrandModel,
  CatalogItemDetailModel,
  CatalogItemModel,
  CatalogTypeModel,
} from "@/models/catalog-item";
import { PaginatedList } from "@/models/paginated-list";

const catalogApiUrl = process.env.CATALOG_API_URL;

const CatalogApi = {
  async getCatalogItems(
    pageNumber: number,
    pageSize: number,
    brandId?: number,
    typeId?: number,
    search?: string
  ): Promise<PaginatedList<CatalogItemModel>> {
    const response = await fetch(
      `${catalogApiUrl}/api/catalog/items?pageNumber=${pageNumber}&pageSize=${pageSize}&brand=${brandId}&type=${typeId}&search=${search}`
    );
    console.log(response.url);
    const paginatedList: PaginatedList<CatalogItemModel> =
      await response.json();
    return {
      ...paginatedList,
      items: paginatedList.items.map((item: CatalogItemModel) => ({
        ...item,
        pictureUri: `${catalogApiUrl}/api/catalog/items/${item.id}/pic`,
      })),
    };
  },
  async getCatalogBrands(): Promise<Array<CatalogBrandModel>> {
    const response = await fetch(`${catalogApiUrl}/api/catalog/brands`);
    return await response.json();
  },
  async getCatalogTypes(): Promise<Array<CatalogTypeModel>> {
    const response = await fetch(`${catalogApiUrl}/api/catalog/types`);
    return await response.json();
  },
  async getCatalogItem(id: number): Promise<CatalogItemDetailModel> {
    const response = await fetch(`${catalogApiUrl}/api/catalog/items/${id}`);
    const catalogItem: CatalogItemDetailModel = await response.json();
    return {
      ...catalogItem,
      pictureUri: `${catalogApiUrl}/api/catalog/items/${id}/pic`,
    };
  }
};

export default CatalogApi;
