"use client"
import { Rotate3D } from "lucide-react";
import { Input } from "./ui/input";
import { useSearchParams } from "next/navigation"
import { useRouter } from 'next/navigation'
import { useEffect, useState } from "react";

const SearchProduct = (
) => {
  const router = useRouter()
  const searchParams = useSearchParams();
  const [searchValue, setSearchValue] = useState("");

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      e.preventDefault();
      const current = new URLSearchParams(Array.from(searchParams.entries()));
      current.set('search', searchValue);
      router.push(`/?${current.toString()}`);
    }
  }

  useEffect(() => {
    const currentSearchValue = searchParams.get('search');
    if (currentSearchValue) {
      setSearchValue(currentSearchValue)
    }
  }, [searchParams]);

  return (
    <Input
      type="search"
      placeholder="Search products..."
      className="pl-8 sm:w-[300px] md:w-[200px] lg:w-[300px]"
      value={searchValue}
      onChange={e => setSearchValue(e.target.value)}
      onKeyDown={handleKeyDown}
    />
  )
}
export default SearchProduct;