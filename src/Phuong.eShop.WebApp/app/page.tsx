import CatalogItemList from "@/components/catalog/catalog-item-list";
import CatalogSearch from "@/components/catalog/catalog-search";
import CatalogApi from "@/lib/api/catalog-api";
import Image from "next/image";

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
    <div>
      <div className="flex flex-col justify-center relative">
        <Image src="/header-home.webp" alt="Northern Mountains" width={1920} height={600} quality={100} className="w-full object-cover object-center" />
        <div className="absolute bottom-10 left-10">
          <h1 className="text-5xl font-bold text-black pb-2">
            Ready for a new adventure?
          </h1>
          <p className="text-2xl font-bold leading-snug text-black">
            Start the season with the latest in clothing and equipment.
          </p>
        </div>
      </div>
      <div className="flex flex-row items-center justify-between w-full max-w-screen-2xl m-auto font-mono py-4">
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
      </div>
    </div>
  );
}
