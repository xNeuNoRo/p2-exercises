import { handleApiError } from "@/helpers/handleApiError";
import { validateApiRes } from "@/helpers/validateApiRes";
import { api } from "@/lib/axios";
import {
  CreatePetFormData,
  Pet,
  PetSchema,
  PetsSchema,
  Species,
  SpeciesListSchema,
  UpdatePetFormData,
} from "@/schemas/pet";

export type PetsAPIType = {
  UpdatePetFormData: UpdatePetFormData & { id: Pet["id"] };
};

/**
 * @description Función para obtener la lista de mascotas desde la API.
 * @returns {Pet[]} Un array de mascotas validado y tipado según el esquema PetsSchema, si la respuesta de la API es correcta. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 */
export async function getPets(): Promise<Pet[]> {
  try {
    // Hacemos la petición a la API para obtener las mascotas
    const { data } = await api.get("/pets");
    // Validamos la respuesta de la API usando el esquema de Zod.
    // Si la validación falla o si la API responde con un error, se lanzará un error con un mensaje específico.
    return validateApiRes(data, PetsSchema);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}

/**
 * @description Función para obtener una mascota por su ID desde la API.
 * @param id El ID de la mascota que se desea obtener. Este ID debe ser un número válido que corresponda a una mascota existente en la base de datos de la API.
 * @returns {Pet} Un objeto de tipo Pet validado y tipado según el esquema PetSchema, si la respuesta de la API es correcta. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 */
export async function getPetById(id: Pet["id"]): Promise<Pet> {
  try {
    // Hacemos la petición a la API para obtener la mascota por su ID
    const { data } = await api.get(`/pets/${encodeURIComponent(id)}`);
    // Validamos la respuesta de la API usando el esquema de Zod.
    // Si la validación falla o si la API responde con un error, se lanzará un error con un mensaje específico.
    return validateApiRes(data, PetSchema);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}

/**
 * @description Función para crear una nueva mascota en la API. Toma un objeto con los datos de la mascota a crear, realiza una petición POST a la API y devuelve el objeto de la mascota creada validado y tipado según el esquema PetSchema. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 * @param petData Los datos de la mascota a crear.
 * @returns {Pet} Un objeto de tipo Pet validado y tipado según el esquema PetSchema, si la respuesta de la API es correcta. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 */
export async function createPet(petData: CreatePetFormData): Promise<Pet> {
  try {
    // Hacemos la petición a la API para crear una nueva mascota
    const { data } = await api.post("/pets", petData);
    // Validamos la respuesta de la API usando el esquema de Zod.
    // Si la validación falla o si la API responde con un error, se lanzará un error con un mensaje específico.
    return validateApiRes(data, PetSchema);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}

/**
 * @description Función para actualizar una mascota existente en la API. Toma un objeto con los datos de la mascota a actualizar, realiza una petición PUT a la API y devuelve el objeto de la mascota actualizada validado y tipado según el esquema PetSchema. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 * @param petData Los datos de la mascota a actualizar, incluyendo su ID. El ID es necesario para identificar qué mascota se desea actualizar en la base de datos de la API.
 */
export async function updatePet(petData: PetsAPIType["UpdatePetFormData"]) {
  try {
    // Hacemos la petición a la API para actualizar una mascota existente
    await api.put(`/pets/${encodeURIComponent(petData.id)}`, petData);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}

/**
 * @description Función para eliminar una mascota existente en la API. Toma el ID de la mascota a eliminar, realiza una petición DELETE a la API y no devuelve ningún dato. Si ocurre un error durante la petición, se lanzará un error con un mensaje específico.
 * @param id El ID de la mascota que se desea eliminar. Este ID debe ser un número válido que corresponda a una mascota existente en la base de datos de la API.
 */
export async function deletePet(id: Pet["id"]) {
  try {
    // Hacemos la petición a la API para eliminar una mascota existente
    await api.delete(`/pets/${encodeURIComponent(id)}`);
  } catch (err) {
    // Si ocurre un error durante la petición, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}

/**
 * @description Función para obtener la lista de especies de mascotas desde la API. Realiza una petición GET a la API y devuelve un array de especies validado y tipado según el esquema SpeciesListSchema. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 * @returns {Species[]} Un array de especies validado y tipado según el esquema SpeciesListSchema, si la respuesta de la API es correcta. Si ocurre un error durante la petición o la validación, se lanzará un error con un mensaje específico.
 */
export async function getSpeciesList(): Promise<Species[]> {
  try {
    // Hacemos la petición a la API para obtener la lista de especies
    const { data } = await api.get("/pets/species");
    // Validamos la respuesta de la API usando el esquema de Zod.
    // Si la validación falla o si la API responde con un error, se lanzará un error con un mensaje específico.
    return validateApiRes(data, SpeciesListSchema);
  } catch (err) {
    // Si ocurre un error durante la petición o la validación, lo manejamos con la función handleApiError.
    handleApiError(err);
  }
}
