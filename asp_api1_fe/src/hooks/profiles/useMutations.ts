import { updateProfileAction } from "@/actions/profile-actions";
import { profileKeys } from "@/lib/query-keys";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

// Hook para actualizar un perfil existente
export function useUpdateProfile() {
  // Obtenemos el cliente de consultas de React Query para poder manipular la cache de consultas
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateProfileAction,
    onSuccess: () => {
      // Mostramos un mensaje de éxito al usuario después de actualizar el perfil
      toast.success(`Perfil actualizado exitosamente!`);

      // Invalidamos la query de los perfiles para que se vuelva a consultar
      // en segundo plano y mostrar los datos actualizados
      queryClient.invalidateQueries({ queryKey: profileKeys.all });
    },
  });
}
