"use client";

import { getProfileById, getProfiles } from "@/api/ProfilesAPI";
import { profileKeys } from "@/lib/query-keys";
import { Profile } from "@/schemas/profile";
import { useQuery } from "@tanstack/react-query";

// Hook para obtener la lista de perfiles
export function useProfiles() {
  return useQuery({
    queryKey: profileKeys.lists(),
    queryFn: getProfiles,
    staleTime: 1000 * 60 * 5, // 5 minutos
  });
}

// Hook para obtener un perfil por su ID
export function useProfile(id: Profile["id"]) {
  return useQuery({
    queryKey: profileKeys.detail(id),
    queryFn: () => getProfileById(id),
    enabled: !!id, // Solo ejecutar la consulta si el ID es válido
  });
}
