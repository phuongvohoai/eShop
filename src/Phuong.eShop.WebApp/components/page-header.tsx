import Image from "next/image";
import Link from "next/link";

const PageHeader = () => {
  return (
    <header className="relative max-w-screen-2xl mx-auto">
      <div className="absolute w-full overflow-hidden">
        <Image src="/header-home.webp" alt="Northern Mountains" width={1920} height={600} quality={100} className="w-full object-cover object-center" />
      </div>
      <div className="relative mx-40 min-h-[480px]">
        <nav className="flex items-center justify-start pt-5 gap-6">
          <Link href={"/"}>
            <Image src="/logo-header.svg" alt="Northern Mountains" width={102} height={88} quality={100} className="mr-auto" />
          </Link>
        </nav>
        <div className="absolute bottom-12 max-w-xl whitespace-nowrap">
          <h1 className="text-5xl font-bold text-black pb-2">
            Ready for a new adventure?
          </h1>
          <p className="text-2xl font-bold leading-snug text-black">
            Start the season with the latest in clothing and equipment.
          </p>
        </div>
      </div>
    </header>
  );
}

export default PageHeader;