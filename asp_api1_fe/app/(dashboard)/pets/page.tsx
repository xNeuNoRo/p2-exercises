import {
  HydrationBoundary,
  QueryClient,
  dehydrate,
} from "@tanstack/react-query";
import PetsClient from "@/components/pets/PetsClient";
import { petKeys } from "@/lib/query-keys";
import { getPets } from "@/api/PetsAPI";

export const metadata = {
  title: "PetApp | Mascotas",
  description: "Gestión de mascotas",
};

export default async function PetsPage() {
  // Creamos una instancia de QueryClient para manejar el estado de las consultas
  const queryClient = new QueryClient();

  // Prefetch de los datos de mascotas para que estén disponibles inmediatamente al renderizar la página
  await queryClient.prefetchQuery({
    queryKey: petKeys.lists(),
    queryFn: getPets,
  });

  return (
    <HydrationBoundary state={dehydrate(queryClient)}>
      <div className="space-y-6">
        {/* Cabecera de la página */}
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div>
            <h1 className="text-3xl font-bold tracking-tight text-gray-900">
              Mis Mascotas
            </h1>
            <p className="mt-1 text-sm text-gray-500">
              Administra el registro de tus compañeros peludos (y no tan
              peludos).
            </p>
          </div>
        </div>

        {/* Componente Cliente que maneja la tabla y el Modal */}
        <PetsClient />
      </div>
    </HydrationBoundary>
  );
}
