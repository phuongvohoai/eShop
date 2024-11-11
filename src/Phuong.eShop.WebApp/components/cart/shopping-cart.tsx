"use client"
import {
  Sheet,
  SheetClose,
  SheetContent,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet"
import { ShoppingBag } from "lucide-react"
import { Button } from "@/components/ui/button"
import CartItem from "./cart-item"
import { useSnapshot } from "valtio"
import { store } from "./cart-store"
import { Badge } from "../ui/badge"
import { useRouter } from "next/navigation"


export default function ShoppingCart() {
  const { itemList } = useSnapshot(store)
  const itemCount = itemList.length
  const totalPrice = itemList.reduce((total, item) => total + item.price * item.quantity, 0)
  const discount = 10;
  const router = useRouter()
  return (
    <Sheet>
      <SheetTrigger asChild>
        <Button variant="secondary" size="icon" className="rounded-full relative">
          <ShoppingBag className="h-5 w-5" />
          <span className="sr-only">Go to cart</span>
          {itemCount > 0 && (
            <span className="absolute -top-1 -right-1 bg-red-500 text-white rounded-full text-xs w-4 h-4 flex items-center justify-center">
              {itemCount}
            </span>
          )}
        </Button>
      </SheetTrigger>
      <SheetContent className="sm:max-w-lg  max-h-full flex flex-col h-screen	">
        <SheetHeader className="flex flex-col">
          <SheetTitle className="text-2xl">Shopping Cart</SheetTitle>
        </SheetHeader>
        <div className="flex flex-col gap-2 px-4 mt-4 flex-grow overflow-auto">
            {itemList.map((item) =>
              <CartItem key={item.id} {...item} />
            )}
          </div>
        <div className="flex flex-col  gap-2 px-4 mt-4 mb-4">
          <div className="flex justify-between">
            <span>Subtotal</span>
            <span>${totalPrice.toFixed(2)}</span>
          </div>
          <div className="flex justify-between">
            <span>Discount</span>
            <span>-${discount}</span>
          </div>
          <div className="border-t my-2"></div> {/* Dòng ngăn cách nhỏ */}
          <div className="flex justify-between font-semibold">
            <span>Total</span>
            <span>${(totalPrice - discount).toFixed(2)}</span>
          </div>
        </div>
        <SheetFooter>
          <SheetClose asChild className="w-full ">
            <Button type="submit" className="bg-slate-700	hover:bg-green-400" onClick={() => router.push("/cart")}>Checkout</Button>
          </SheetClose>
        </SheetFooter>
      </SheetContent>
    </Sheet>
  )
}
