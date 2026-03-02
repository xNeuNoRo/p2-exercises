import { getPetById, getPets, getSpeciesList } from "@/api/PetsAPI";
import { petKeys } from "@/lib/query-keys";
import { Pet } from "@/schemas/pet";
import { useQuery } from "@tanstack/react-query";

// Hook para obtener la lista de mascotas
export function usePets() {
  return useQuery({
    queryKey: petKeys.lists(),
    queryFn: getPets,
    staleTime: 1000 * 60 * 5, // 5 minutos
  });
}

// Hook para obtener una mascota por su ID
export function usePet(id: Pet["id"]) {
  return useQuery({
    queryKey: petKeys.detail(id),
    queryFn: () => getPetById(id),
    enabled: !!id, // Solo ejecutar la consulta si el ID es válido
  });
}

// Hook para obtener la lista de especies de mascotas
export function usePetSpeciesList() {
  return useQuery({
    queryKey: petKeys.species(),
    queryFn: getSpeciesList,
    staleTime: 1000 * 60 * 60, // 1 hora
  });
}
