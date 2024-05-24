import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet"
import { ShoppingBag } from "lucide-react"
import { Button } from "@/components/ui/button"
import CartItem from "./cart-item"


export default function ShoppingCart() {
  return (
    <Sheet>
      <SheetTrigger asChild>
        <Button variant="secondary" size="icon" className="rounded-full">
          <ShoppingBag className="h-5 w-5" />
          <span className="sr-only">Go to cart</span>
        </Button>
      </SheetTrigger>
      <SheetContent className="sm:max-w-lg">
        <SheetHeader>
          <SheetTitle className="text-2xl">Shopping Cart</SheetTitle>
          <SheetDescription className="flex flex-col gap-2">
            <CartItem />
            <CartItem />
            <CartItem />
            <CartItem />
          </SheetDescription>
        </SheetHeader>
      </SheetContent>
    </Sheet>
  )
}
