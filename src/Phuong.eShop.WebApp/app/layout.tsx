import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import PageHeader from "@/components/page-header";
import PageFooter from "@/components/page-footer";

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
      <body className={inter.className}>
          <PageHeader />
          {children}
          <PageFooter />
      </body>
    </html>
  );
}
