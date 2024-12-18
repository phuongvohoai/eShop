'use client'
import { cn } from "@/lib/utils";
import { Minus, Plus } from "lucide-react";
import { Input } from "@/components/ui/input";
import { useState } from "react";
import { decrementItemQuantity, incrementItemQuantity, removeItemFromCart } from "./cart-store";

export default function QuantitySelector(props: {
  quantity: number;
  maxQuantity?: number;
  id: number
}) {
  const { maxQuantity } = props;
  const [currentQuantity, setCurrentQuantity] = useState<number>(props.quantity);
  const increment = () => {
    if (maxQuantity && currentQuantity == maxQuantity) return;
    const newQuantity = currentQuantity + 1;
    setCurrentQuantity(newQuantity);
    incrementItemQuantity(props.id)
  };
  const decrement = () => {
    const newQuantity = currentQuantity - 1;
    setCurrentQuantity(newQuantity);
    decrementItemQuantity(props.id)
    if (newQuantity == 0) {
      removeItemFromCart(props.id)
    }
  }
  return (
    <div className="border p-1 gap-1 flex justify-around items-center w-32">
      <Minus size={16} onClick={decrement} className={
        cn("shrink-0", {
          "cursor-not-allowed text-gray-400": currentQuantity == 0
        })
      } />
      <Input
        value={currentQuantity}
        onChange={(e) => setCurrentQuantity(+e.target.value)}
        type="number"
        className={cn(
          "mx-2 h-7 border-none text-center focus-visible:ring-offset-0 focus-visible:ring-0",
          "[appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
        )}
      />
      <Plus size={16} onClick={increment}
        className={cn("shrink-0", {
          "cursor-not-allowed text-gray-400": currentQuantity === maxQuantity
        })} />
    </div>
  )
}
