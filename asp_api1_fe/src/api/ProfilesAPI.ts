import { handleApiError } from "@/helpers/handleApiError";
import { validateApiRes } from "@/helpers/validateApiRes";
import { api } from "@/lib/axios";
import {
  Profile,
  ProfileSchema,
  ProfilesSchema,
  UpdateProfileFormData,
} from "@/schemas/profile";

export type ProfilesAPIType = {
  UpdateProfileFormData: UpdateProfileFormData & { id: Profile["id"] };
};

/**
 * @description Función para obtener la lista de perfiles desde la API. Realiza una petición GET a la API y devuelve un array de perfiles validado y tipado según el esquema ProfilesSchema. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 * @returns {Profile[]} Un array de perfiles validado y tipado según el esquema ProfilesSchema, si la respuesta de la API es correcta. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 */
export async function getProfiles(): Promise<Profile[]> {
  try {
    // Hacemos la petición a la API para obtener los perfiles
    const { data } = await api.get("/profiles");
    // Validamos la respuesta de la API usando el esquema de Zod.
    // Si la validación falla o si la API responde con un error, se lanzará un error con un mensaje específico.
    return validateApiRes(data, ProfilesSchema);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}

/**
 * @description Función para obtener un perfil por su ID desde la API. Realiza una petición GET a la API con el ID del perfil y devuelve el objeto de perfil validado y tipado según el esquema ProfileSchema. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 * @param id El ID del perfil que se desea obtener. Este ID debe ser un número válido que corresponda a un perfil existente en la base de datos de la API.
 * @returns {Profile} Un objeto de tipo Profile validado y tipado según el esquema ProfileSchema, si la respuesta de la API es correcta. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 */
export async function getProfileById(id: Profile["id"]): Promise<Profile> {
  try {
    // Hacemos la petición a la API para obtener el perfil por su ID
    const { data } = await api.get(`/profiles/${encodeURIComponent(id)}`);
    // Validamos la respuesta de la API usando el esquema de Zod.
    // Si la validación falla o si la API responde con un error, se lanzará un error con un mensaje específico.
    return validateApiRes(data, ProfileSchema);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}

/**
 * @description Función para actualizar un perfil en la API. Toma un objeto con los datos del perfil a actualizar, realiza una petición PUT a la API con el ID del perfil y devuelve el objeto de perfil actualizado validado y tipado según el esquema ProfileSchema. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 * @param profileData Los datos del perfil a actualizar, incluyendo el ID del perfil. El ID es necesario para identificar qué perfil se desea actualizar en la base de datos de la API.
 */
export async function updateProfile(
  profileData: ProfilesAPIType["UpdateProfileFormData"],
): Promise<void> {
  try {
    // Hacemos la petición a la API para actualizar el perfil
    await api.put(`/profiles/${profileData.id}`, profileData);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}
