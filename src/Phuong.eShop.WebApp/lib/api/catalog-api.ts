import { CatalogItemCartDetailModel } from "@/components/cart/cart-store";
import {
  CatalogBrandModel,
  CatalogItemDetailModel,
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
  ): Promise<PaginatedList<CatalogItemCartDetailModel>> {
    const response = await fetch(
      `${catalogApiUrl}/api/catalog/items?pageNumber=${pageNumber}&pageSize=${pageSize}&brand=${brandId}&type=${typeId}&searchString=${search}`
    );
    console.log(catalogApiUrl);
    const paginatedList: PaginatedList<CatalogItemCartDetailModel> =
      await response.json();
      console.log('paginatedList', paginatedList)
    return {
      ...paginatedList,
      items: paginatedList.items.map((item: CatalogItemCartDetailModel) => ({
        ...item,
        pictureUri: `${catalogApiUrl}/${item.pictureUri}`,
      })),
    };
  },
  async getCatalogBrands(): Promise<Array<CatalogBrandModel>> {
    const response = await fetch(`${catalogApiUrl}/api/catalog/brands`);
    const jsonData = await response.json();
    console.log('getCatalogBrands', jsonData)
    return jsonData.value;
  },
  async getCatalogTypes(): Promise<Array<CatalogTypeModel>> {
    const response = await fetch(`${catalogApiUrl}/api/catalog/types`);
    const jsonData = await response.json();
    return jsonData.value;
  },
  async getCatalogItem(id: number): Promise<CatalogItemDetailModel> {
    const response = await fetch(`${catalogApiUrl}/api/catalog/items/${id}`);
    const catalogItem: CatalogItemDetailModel = await response.json(); 
 
    // pics folder
    // api/catalog/items/1/pic
    // if picture in db /api/files/2/content
    return {
      ...catalogItem,
      pictureUri: `${catalogApiUrl}/${catalogItem.pictureUri}`,
    };
  }
};

export default CatalogApi;
