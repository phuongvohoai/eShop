import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination"
import { DOTS, usePagination } from "@/lib/hook/use-pagination";
import { cn } from "@/lib/utils";

type SmartPaginationProps = {
  totalPage: number;
  siblingCount?: number;
  currentPage: number;
}

export function SmartPagination({
  siblingCount = 1,
  currentPage,
  totalPage
}: SmartPaginationProps) {
  const paginationRange = usePagination({
    currentPage,
    siblingCount,
    totalPage
  });

  if (!paginationRange) return null;

  let lastPage = +paginationRange[paginationRange.length - 1];

  const getPageUrl = (page: number) => {
    return `?page=${page}`;
  }

  return (
    <Pagination>
      <PaginationContent>
        {currentPage > 1 &&
          <PaginationItem>
            <PaginationPrevious href={getPageUrl(currentPage - 1)} />
          </PaginationItem>
        }
        {paginationRange.map(pageNumber => {
          if (pageNumber === DOTS) {
            return (
              <PaginationItem key={pageNumber}>
                <PaginationEllipsis />
              </PaginationItem>
            );
          }
          return (
            <PaginationItem key={pageNumber}>
              <PaginationLink href={getPageUrl(+pageNumber)} isActive={currentPage === +pageNumber}>{pageNumber}</PaginationLink>
            </PaginationItem>
          );
        })}
        {currentPage < lastPage &&
          <PaginationItem>
            <PaginationNext href={getPageUrl(currentPage + 1)} />
          </PaginationItem>
        }
      </PaginationContent>
    </Pagination>
  )
}
