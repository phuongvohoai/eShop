export interface CatalogBrandModel {
  id: number;
  name: string;
}

export interface CatalogTypeModel {
  id: number;
  name: string;
}

export interface CatalogItemModel {
  id: number;
  name: string;
  pictureUri: string;
  price: number;
}

export interface CatalogItemDetailModel extends CatalogItemModel {
  catalogBrand: string;
  catalogType: string;
  description?: string;
  availableStock: number;
}
