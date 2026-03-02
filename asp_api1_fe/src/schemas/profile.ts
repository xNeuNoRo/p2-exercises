import { z } from "zod";

export const ProfileSchema = z.object({
  id: z.number(),
  name: z.string().min(1, "El nombre es obligatorio"),
  career: z.string().min(1, "La carrera es obligatoria"),
  studentId: z.string().min(1, "La matricula estudiantil es obligatoria"),
});

export const ProfilesSchema = z.array(ProfileSchema);

export const UpdateProfileSchema = ProfileSchema.pick({
  name: true,
  career: true,
  studentId: true,
});

// Tipos inferidos a partir de los esquemas
export type Profile = z.infer<typeof ProfileSchema>;
export type UpdateProfileFormData = z.infer<typeof UpdateProfileSchema>;
