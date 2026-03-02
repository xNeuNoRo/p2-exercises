import { ProfilesAPIType, updateProfile } from "@/api/ProfilesAPI";
import { revalidatePath } from "next/cache";

// Todas estas funciones son los Server Actions de Next.js que se encargaran de lo relacionado con escritura en la API

// Actualizar un perfil existente
export async function updateProfileAction(
  data: ProfilesAPIType["UpdateProfileFormData"],
) {
  // Llamamos a la función de la API para actualizar el perfil con los datos proporcionados
  await updateProfile(data);
  // Después de actualizar el perfil, revalidamos la página de perfiles para mostrar los cambios en la lista
  revalidatePath("/");
}
