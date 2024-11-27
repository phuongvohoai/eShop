"use client"
import { ShoppingBag } from "lucide-react";
import { Button } from "@/components/ui/button";;
import { addItemIntoCart, CatalogItemCartDetailModel, useCartStore } from "../cart/cart-store";
import { useToast } from "@/hooks/use-toast";
import { getCurrentUser } from "../login/login-api";
import { jwtDecode, JwtPayload } from "jwt-decode";


export default function AddToCartButton(props: CatalogItemCartDetailModel) {
    const { itemList } = useCartStore()
    const { toast } = useToast()
    const handleAdd = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        addItemIntoCart(props)
        toast({
            title: "Added to cart successfully",
            variant: "access"
        });
        saveCart();
    }
    const saveCart = async () => {
        const currentUser = await getCurrentUser();
        const accessToken = currentUser?.accessToken
        console.log(accessToken)
        const decoded = accessToken ? jwtDecode<JwtPayload>(accessToken, { header: true }) : null;
        console.log(decoded)
        // if (currentUser)
        //     {
        //         const response = await fetch('http://localhost:50211/api/cart', {
        //             method: 'POST',
        //             headers: {
        //                 "Accept": "application/json",
        //                 "Content-Type": "application/json"
        //             },
        //             body: JSON.stringify({ currentUser.accessToken, itemList }),
        //         });
        //     }
    }

    return (
        <Button className="w-full" onClick={handleAdd}>
            <ShoppingBag className="mr-2 h-4 w-4" /> Add to cart
        </Button>
    )
}

