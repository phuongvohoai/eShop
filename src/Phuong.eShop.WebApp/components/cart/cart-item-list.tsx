"use client";
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { Select } from "../ui/select";
import Link from "next/link";
import Image from "next/image";
import { removeItemFromCart, useCartStore } from "./cart-store";
import QuantitySelector from "./quantity-selector";
import { useRouter } from "next/navigation";
import { Trash2 } from "lucide-react";


export default function CartItemList() {
    const { itemList } = useCartStore()
    const totalPrice = itemList.reduce((total, item) => total + item.price * item.quantity, 0)
    const router = useRouter();
    return (
        <div className="container mx-auto py-8 px-4 relative">
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-bold">
                    Shopping Cart <span className="text-gray-500">({itemList.length})</span>
                </h1>
                <button
                    onClick={() => router.push("/")}
                    className="text-purple-600 text-sm hover:text-red-500"
                >
                    ← Continue Shopping
                </button>
            </div>
            <div className="grid grid-cols-3 gap-6">
                <div className="col-span-2">
                    <Table>
                        <TableHeader className="">
                            <TableRow>
                                <TableHead>PRODUCT</TableHead>
                                <TableHead>PRICE</TableHead>
                                <TableHead>QUANTITY</TableHead>
                                <TableHead className="text-right">TOTAL</TableHead>
                                <TableHead></TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {itemList.map((item) => (
                                <TableRow key={item.id}>
                                    <TableCell >
                                        <div className="flex gap-4">
                                            <div className="w-30 shrink-0">
                                                <Link href={`/catalog/${item.id}`}>
                                                    <Image
                                                        src={item.pictureUri}
                                                        alt={item.name}
                                                        width={120}
                                                        height={120}
                                                        className="object-cover object-center"
                                                    />
                                                </Link>
                                            </div>
                                            <div className="flex flex-1 flex-col text-lg">
                                                <span className="uppercase">{item.name}</span>
                                                <span className="italic" >{item.catalogBrand}</span>
                                            </div>
                                        </div>
                                    </TableCell>
                                    <TableCell>${item.price}</TableCell>
                                    <TableCell>
                                        <QuantitySelector quantity={item.quantity} maxQuantity={item.availableStock} id={item.id} />
                                    </TableCell>
                                    <TableCell className="text-right">${(item.quantity * item.price).toFixed(2)}</TableCell>
                                    <TableCell>
                                        <Trash2 className="cursor-pointer hover:text-red-500" onClick={()=>{removeItemFromCart(item.id)}} />
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </div>
                <div className="p-6 rounded-lg bg-slate-100 h-fit sticky top-20">
                    <h2 className="text-lg font-bold mb-4">Order Summary</h2>
                    <div className="flex justify-between mb-2">
                        <span>Items ({itemList.length})</span>
                        <span>${totalPrice.toFixed(2)}</span>
                    </div>
                    <div className="flex justify-between mb-2">
                        <span>Shipping</span>
                        <span className="font-semibold">FREE!</span>
                    </div>
                    <div className="mt-4">
                        <h3 className="font-semibold mb-2">Discount Code</h3>
                        <div className="flex space-x-2">
                            <Input placeholder="Enter your code" />
                            <Button variant="default" className=" text-white">
                                Apply
                            </Button>
                        </div>
                    </div>
                    <div className="flex justify-between font-bold mt-6">
                        <span>Total Cost</span>
                        <span>${totalPrice.toFixed(2)}</span>
                    </div>
                    <Button variant="default" className="w-full mt-4 text-white">
                        Checkout
                    </Button>
                </div>
            </div>
        </div>
    );
}
