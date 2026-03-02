import type { ReactNode } from "react";

// Componente simple para mostrar mensajes de error de forma consistente en la app
export default function ErrorMessage({
  children,
}: Readonly<{ children: ReactNode }>) {
  return (
    <div className="p-3 text-xs font-bold text-center text-red-600 uppercase bg-red-50 border border-red-200 rounded-lg animate-in fade-in slide-in-from-top-1">
      {children}
    </div>
  );
}