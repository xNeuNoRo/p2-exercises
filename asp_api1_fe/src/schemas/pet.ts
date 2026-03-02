import { z } from "zod";

export const PetSchema = z.object({
  id: z.number(),
  name: z
    .string()
    .min(1, "El nombre de la mascota es obligatorio")
    .max(100, "El nombre de la mascosta no puede tener más de 100 caracteres"),
  species: z.union([
    z.enum([
      "Dog",
      "Cat",
      "Rabbit",
      "Hamster",
      "Bird",
      "Reptile",
      "Amphibian",
      "Fish",
      "Invertebrate",
      "Other",
    ]),
    z.string(),
  ]),
  speciesId: z.number(),
  speciesName: z.string(),
  race: z
    .string()
    .min(1, "La raza es obligatoria")
    .max(100, "La raza no puede tener más de 100 caracteres"),
  age: z
    .number()
    .min(0, "La edad debe ser mayor o igual a 0")
    .max(20, "La edad máxima permitida es 20 años"),
});

export const PetsSchema = z.array(PetSchema);

export const SpeciesSchema = z.object({
  id: z.number(),
  name: z.string(),
});

export const SpeciesListSchema = z.array(SpeciesSchema);

export const CreatePetSchema = PetSchema.pick({
  name: true,
  race: true,
  age: true,
}).extend({
  species: z.number({ error: "El campo especie es obligatorio" }), // En la creación, species es un numero que representa el enum en el backend
});

// Para la actualizacion de una mascota, sera lo mismo.
export const UpdatePetSchema = CreatePetSchema;

// Tipos inferidos a partir de los esquemas
export type Pet = z.infer<typeof PetSchema>;
export type Species = z.infer<typeof SpeciesSchema>;
export type CreatePetFormData = z.infer<typeof CreatePetSchema>;
export type UpdatePetFormData = z.infer<typeof UpdatePetSchema>;
