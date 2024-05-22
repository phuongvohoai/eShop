import CatalogItemList from "@/components/catalog/catalog-item-list";
import CatalogSearch from "@/components/catalog/catalog-search";
import CatalogApi from "@/lib/api/catalog-api";

export default async function Home({
  searchParams,
}: {
  searchParams: { [key: string]: string | string[] | undefined };
}) {
  const [catalogItemList, catalogBrands, catalogTypes] = await Promise.all([
    CatalogApi.getCatalogItems(
      searchParams.page ? +searchParams.page : 1,
      12,
      searchParams.brand ? +searchParams.brand : 0,
      searchParams.type ? +searchParams.type : 0,
      searchParams.search ? searchParams.search as string : ''),
    CatalogApi.getCatalogBrands(),
    CatalogApi.getCatalogTypes(),
  ]);

  return (
    <main className="flex flex-row items-center justify-between w-full max-w-screen-2xl m-auto font-mono py-4">
      <aside className="w-[350px] self-start px-4 shrink-0">
        <CatalogSearch
          catalogBrands={catalogBrands}
          catalogTypes={catalogTypes}
        />
      </aside>
      <CatalogItemList
        catalogItems={catalogItemList.items}
        currentPage={catalogItemList.pageNumber}
        totalPage={catalogItemList.totalPages}
      ></CatalogItemList>
    </main>
  );
}
