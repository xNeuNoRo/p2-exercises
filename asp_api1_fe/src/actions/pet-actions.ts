import { createPet, deletePet, PetsAPIType, updatePet } from "@/api/PetsAPI";
import { CreatePetFormData, Pet } from "@/schemas/pet";
import { revalidatePath } from "next/cache";

// Todas estas funciones son los Server Actions de Next.js que se encargaran de lo relacionado con escritura en la API

// Crear una nueva mascota
export async function createPetAction(data: CreatePetFormData): Promise<Pet> {
  // Llamamos a la función de la API para crear una nueva mascota con los datos proporcionados
  const pet = await createPet(data);
  // Después de crear la mascota, revalidamos la página de mascotas para mostrar la nueva mascota en la lista
  revalidatePath("/pets");
  // Retornamos la nueva mascota creada para que pueda ser usada en la interfaz si es necesario
  return pet;
}

// Actualizar una mascota existente
export async function updatePetAction(data: PetsAPIType["UpdatePetFormData"]) {
  // Llamamos a la función de la API para actualizar la mascota con los datos proporcionados
  await updatePet(data);
  // Después de actualizar la mascota, revalidamos la página de mascotas para mostrar los cambios en la lista
  revalidatePath("/pets");
}

// Eliminar una mascota existente
export async function deletePetAction(id: Pet["id"]) {
  // Llamamos a la función de la API para eliminar la mascota con el ID proporcionado
  await deletePet(id);
  // Después de eliminar la mascota, revalidamos la página de mascotas para actualizar la lista y mostrar que la mascota ha sido eliminada
  revalidatePath("/pets");
}
