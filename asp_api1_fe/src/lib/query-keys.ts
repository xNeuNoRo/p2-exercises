// Este archivo define las query keys para las consultas con React Query
// (Asi centralizamos las keys para evitar errores de tipeo y facilitar su mantenimiento)

// Query keys para perfiles
export const profileKeys = {
  all: ["profile"] as const,
  lists: () => [...profileKeys.all, "list"] as const,
  detail: (id: number) => [...profileKeys.all, "detail", id] as const,
};

// Query keys para mascotas
export const petKeys = {
  all: ["pets"] as const,
  lists: () => [...petKeys.all, "list"] as const,
  detail: (id: number) => [...petKeys.all, "detail", id] as const,
  species: () => [...petKeys.all, "species"] as const,
};
