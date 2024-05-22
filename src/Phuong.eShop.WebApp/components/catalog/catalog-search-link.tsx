'use client'

import Link from "next/link";
import { useSearchParams } from "next/navigation"

export default function CatalogSearchLink({
  children,
  href,
  queryParam
}: {
  children: React.ReactNode,
  href: string,
  queryParam: {
    name: string,
    value: string
  }
}) {
  const searchParams = useSearchParams();
  const current = new URLSearchParams(Array.from(searchParams.entries()));
  current.set(queryParam.name, queryParam.value);
  return (
    <>
      <Link href={{ pathname: href, search: current.toString() }}>
        {children}
      </Link>
    </>
  )
}
