"use client"
import { CircleUser, Search, ShoppingBag } from "lucide-react";
import Image from "next/image";
import Link from "next/link";
import { Input } from "@/components/ui/input"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Button } from "@/components/ui/button"
import SearchProduct from "./searchProduct";
import { getCurrentUser, UserSession } from "./login/login-api";
import { useContext, useEffect, useState } from "react";
import { deleteCurrentUser } from "@/lib/api/logout-api";
import { useRouter } from 'next/navigation'
import { usePathname } from 'next/navigation'
import { useAuth } from "@/app/context/user-context-provider";
import ShoppingCart from "./cart/shopping-cart";

const PageHeader = () => {
  const router = useRouter();
  const [auth, setAuth] = useAuth();

  const handleLogout = async () => {
    if (auth) {
      setAuth({
        ...auth,
        email: "",
        accessToken: ""
      })
      const deleteCookie = await deleteCurrentUser();
      router.push("/login")
    }
  }

  return (
    <header className="sticky top-0 shadow h-20 z-10 bg-white">
      <nav className="max-w-screen-2xl mx-auto h-full flex items-center">
        <div className="flex items-center gap-6 text-lg font-medium">
          <Link href={"/"}>
            <Image src="/logo-header.svg" alt="Northern Mountains" width={80} height={80} quality={100} className="mr-auto" />
          </Link>
        </div>
        <div className="flex items-center gap-5 md:ml-auto md:gap-2 lg:gap-5">
          <form className="ml-auto flex-1 sm:flex-initial">
            <div className="relative">
              <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
              <SearchProduct></SearchProduct>
            </div>
          </form>
          <ShoppingCart></ShoppingCart>
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="secondary" size="icon" className="rounded-full">
                <CircleUser className="h-5 w-5" />
                <span className="sr-only">Toggle user menu</span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>{auth.accessToken ? auth.email : "My Account"}</DropdownMenuLabel>
              {auth.accessToken && <>
                <DropdownMenuItem>Profile</DropdownMenuItem>
                <DropdownMenuItem>Order</DropdownMenuItem>
              </>
              }
              <DropdownMenuSeparator />
              <DropdownMenuItem>Settings</DropdownMenuItem>
              <DropdownMenuItem>Support</DropdownMenuItem>
              <DropdownMenuSeparator />
              {auth.accessToken ? (<DropdownMenuItem onClick={handleLogout}>Logout</DropdownMenuItem>
              ) : (
                <DropdownMenuItem onClick={() => router.push('/login')}>Login</DropdownMenuItem>
              )}
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      </nav>
    </header>
  );
}
export default PageHeader;
