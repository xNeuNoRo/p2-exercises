import {
  HydrationBoundary,
  QueryClient,
  dehydrate,
} from "@tanstack/react-query";
import ProfileCard from "@/components/profiles/ProfileCard";
import { getProfileById, getProfiles } from "@/api/ProfilesAPI";
import { profileKeys } from "@/lib/query-keys";

export const metadata = {
  title: "PetApp | Mi Perfil",
  description: "Identificación Académica Estudiantil",
};

export default async function HomePage() {
  const queryClient = new QueryClient();

  // Prefetch de la lista de perfiles para que esté disponible inmediatamente al renderizar la página
  await queryClient.prefetchQuery({
    queryKey: profileKeys.lists(),
    queryFn: getProfiles,
  });

  // Prefetch de un perfil específico (en este caso, el perfil con ID 1) para que esté disponible inmediatamente al renderizar la página
  await queryClient.prefetchQuery({
    queryKey: profileKeys.detail(1),
    queryFn: () => getProfileById(1),
  });

  return (
    <HydrationBoundary state={dehydrate(queryClient)}>
      {/* Contenedor principal que centra la tarjeta en la pantalla */}
      <div className="flex min-h-[calc(100vh-8rem)] items-center justify-center p-4">
        <ProfileCard profileId={1} />
      </div>
    </HydrationBoundary>
  );
}
