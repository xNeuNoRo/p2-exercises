import type { Metadata } from "next";
import { Inter } from "next/font/google";
import AppProvider from "@/components/providers/AppProvider";
import "./globals.css";

const inter = Inter({
  subsets: ["latin"],
  variable: "--font-inter",
});

export const metadata: Metadata = {
  title: "ASP.NET API Project",
  description:
    "Proyecto de ejemplo para consumir una API de ASP.NET desde un frontend en Next.js",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="es">
      <body className={`${inter.variable}`}>
        <AppProvider>{children}</AppProvider>
      </body>
    </html>
  );
}
