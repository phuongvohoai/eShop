import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import PageHeader from "@/components/page-header";
import PageFooter from "@/components/page-footer";
import { cn } from "@/lib/utils";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "Northern Mountains"
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={cn(inter.className, "flex flex-col min-h-screen")}>
        <PageHeader />
        <main className="flex flex-1 max-w-screen-2xl mx-auto">
          {children}
        </main>
        <PageFooter />
      </body>
    </html>
  );
}
