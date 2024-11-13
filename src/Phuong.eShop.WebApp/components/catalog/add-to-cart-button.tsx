"use client"
import { ShoppingBag } from "lucide-react";
import { Button } from "@/components/ui/button";;
import { addItemIntoCart, CatalogItemCartDetailModel} from "../cart/cart-store";
import { useToast } from "@/hooks/use-toast";

export default function AddToCartButton(props: CatalogItemCartDetailModel) {
    const { toast } = useToast()
    const handleAdd = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        addItemIntoCart(props)
        toast({
            title: "Added to cart successfully",
            variant: "access"
        });
    }
    return (
        <Button className="w-full" onClick={handleAdd}>
            <ShoppingBag className="mr-2 h-4 w-4" /> Add to cart
        </Button>
    )
}