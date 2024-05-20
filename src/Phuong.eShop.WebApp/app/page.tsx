import CatalogItemList from "@/components/catalog/catalog-item-list";

export default function Home() {
  const catalogItems = [
    {
      id: 1,
      name: "Wanderer Black Hiking Boots",
      pictureUri: "/Pics/1.webp",
      price: 129.99,
    },
    {
      id: 2,
      name: "Summit Pro Harness",
      pictureUri: "/Pics/2.webp",
      price: 192.99,
    },
    {
      id: 3,
      name: "Wanderer Black Hiking Boots",
      pictureUri: "/Pics/3.webp",
      price: 129.99,
    },
    {
      id: 4,
      name: "Alpine Fusion Goggles",
      pictureUri: "/Pics/4.webp",
      price: 129.99,
    },
    {
      id: 5,
      name: "Wanderer Black Hiking Boots",
      pictureUri: "/Pics/5.webp",
      price: 129.99,
    },
  ];

  return (
    <main className="flex flex-row items-center justify-between w-full max-w-screen-2xl m-auto font-mono py-4">
      <aside className="w-[350px] h-96">
        <h1 className="w-[350px] h-[400px]">Aside</h1>
      </aside>
      <CatalogItemList catalogItems={catalogItems}></CatalogItemList>
    </main>
  );
}
