import {
  createPetAction,
  deletePetAction,
  updatePetAction,
} from "@/actions/pet-actions";
import { petKeys } from "@/lib/query-keys";
import { Pet } from "@/schemas/pet";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

// Hook para crear una nueva mascota
export function useCreatePet() {
  // Obtenemos el cliente de consultas de React Query para poder manipular la cache de consultas
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createPetAction,
    onSuccess: (data) => {
      // Mostramos un mensaje de éxito al usuario después de crear la categoría
      toast.success(`Mascota "${data.name}" creada exitosamente!`);

      // Pessimistic update: actualizamos la cache de las mascotas después de que
      // la nueva mascota ha sido creada exitosamente en la API
      queryClient.setQueryData(
        petKeys.lists(),
        (oldData: Pet[] | undefined) => {
          if (!oldData) return [data];
          // Evitamos agregar la mascota a la lista si ya existe
          // (por ejemplo, si la lista ya se ha actualizado con la nueva mascota a través de otra consulta)
          if (oldData.some((pet) => pet.id === data.id)) return oldData;
          return [...oldData, data];
        },
      );

      // Invalidamos la query tambien para que se vuelva a consultar
      // en segundo plano en caso de que haya alguna inconsistencia
      queryClient.invalidateQueries({ queryKey: petKeys.all });
    },
    onError: (err) => {
      // Mostramos un mensaje de error al usuario si ocurre un error al crear la categoría
      toast.error(err.message);
    },
  });
}

// Hook para actualizar una mascota existente
export function useUpdatePet() {
  // Obtenemos el cliente de consultas de React Query para poder manipular la cache de consultas
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updatePetAction,
    onSuccess: () => {
      // Mostramos un mensaje de éxito al usuario después de actualizar la categoría
      toast.success(`Mascota actualizada exitosamente!`);

      // Invalidamos la query de la mascota actual para que se vuelva a consultar
      // en segundo plano y mostrar los datos actualizados
      queryClient.invalidateQueries({ queryKey: petKeys.all });
    },
    onError: (err) => {
      // Mostramos un mensaje de error al usuario si ocurre un error al actualizar la categoría
      toast.error(err.message);
    },
  });
}

// Hook para eliminar una mascota existente
export function useDeletePet() {
  // Obtenemos el cliente de consultas de React Query para poder manipular la cache de consultas
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deletePetAction,
    onSuccess: () => {
      // Mostramos un mensaje de éxito al usuario después de eliminar la categoría
      toast.success(`Mascota eliminada exitosamente!`);

      // Invalidamos la query de las mascotas para que se vuelva a consultar
      // en segundo plano y mostrar la lista actualizada sin la mascota eliminada
      queryClient.invalidateQueries({ queryKey: petKeys.all });
    },
    onError: (err) => {
      // Mostramos un mensaje de error al usuario si ocurre un error al eliminar la categoría
      toast.error(err.message);
    },
  });
}
