"use client";

import { useId } from "react";
import { useForm } from "react-hook-form";
import {
  IoPencil,
  IoCheckmark,
  IoClose,
  IoSchool,
  IoBarcodeOutline,
} from "react-icons/io5";

import { useProfile } from "@/hooks/profiles/useQueries";
import { useUpdateProfile } from "@/hooks/profiles/useMutations";
import { zodResolver } from "@hookform/resolvers/zod";
import { UpdateProfileFormData, UpdateProfileSchema } from "@/schemas/profile";
import { useQueryString } from "@/hooks/shared/useQueryString";
import Link from "next/link";
import { useRouter } from "next/navigation";

export default function ProfileCard({
  profileId,
}: Readonly<{ profileId: number }>) {
  // Generamos un ID único para el formulario, que se usará para asociar labels e inputs de manera accesible.
  const formId = useId();

  // Hooks para manejar la consulta del perfil y la mutación de actualización
  const { data: profile, isLoading } = useProfile(profileId);
  const { mutate: updateProfile, isPending: isUpdatingProfile } =
    useUpdateProfile();

  // Hook personalizado para manejar la generación de URLs con query strings en Next.js
  const { createUrl, searchParams } = useQueryString();
  // Extraemos el router para controlar la apertura del modo de edición
  const router = useRouter();

  // Funciones para habilitar y deshabilitar el modo de edición,
  // que manipulan los parámetros de la URL para controlar el estado del formulario
  const enableEditMode = createUrl({ action: "edit-profile", profileId });
  const disableEditMode = () => {
    const newUrl = createUrl({ action: null, profileId: null });
    router.replace(newUrl, { scroll: false });
  };
  // Determinamos si estamos en modo edición basándonos en los parámetros de búsqueda de la URL
  const isEditing = searchParams.get("action") === "edit-profile";

  // Configuramos React Hook Form con el resolver de Zod para validación y los valores por defecto.
  const { register, handleSubmit, reset } = useForm<UpdateProfileFormData>({
    resolver: zodResolver(UpdateProfileSchema),
    values: profile
      ? {
          name: profile.name,
          career: profile.career,
          studentId: profile.studentId,
        }
      : undefined,
    defaultValues: {
      name: "",
      career: "",
      studentId: "",
    },
  });

  // Si estamos cargando el perfil o no tenemos datos, mostramos un skeleton placeholder.
  if (isLoading || !profile) {
    return (
      <div className="h-[28rem] w-full max-w-3xl animate-pulse rounded-2xl border border-gray-200 bg-white shadow-xl"></div>
    );
  }

  // Función para manejar la cancelación de la edición,
  // que resetea el formulario a los valores originales del perfil y desactiva el modo de edición.
  const handleCancel = () => {
    reset();
    disableEditMode();
  };

  // Función para manejar el envío del formulario de actualización de perfil,
  // que verifica si hubo cambios y llama a la mutación de actualización.
  const onSubmit = (data: UpdateProfileFormData) => {
    // Si no hubo cambios, desactivamos el modo de edición sin hacer la llamada a la API
    if (
      data.name.trim() === profile.name.trim() &&
      data.career.trim() === profile.career.trim() &&
      data.studentId.trim() === profile.studentId.trim()
    ) {
      disableEditMode();
      return;
    }

    // Llamamos a la mutación de actualización con los nuevos datos del perfil,
    // y manejamos los casos de éxito y error con notificaciones.
    updateProfile(
      { id: profileId, ...data },
      { onSuccess: () => disableEditMode() },
    );
  };

  return (
    <div className="relative w-full max-w-3xl overflow-hidden rounded-2xl border border-gray-200 bg-white shadow-2xl transition-all hover:shadow-3xl">
      {/* Banner superior más amplio */}
      <div className="h-40 bg-blue-600 p-8 text-white relative">
        <div className="flex justify-between items-start relative z-10">
          <div className="flex items-center gap-3">
            <IoSchool className="h-8 w-8 opacity-80" />
            <span className="text-base font-bold tracking-widest opacity-90 uppercase">
              Instituto Tecnológico de las Américas (ITLA)
            </span>
          </div>

          {!isEditing && (
            <Link
              href={enableEditMode}
              className="rounded-full bg-white/20 p-2.5 text-white backdrop-blur-sm transition-colors hover:bg-white/30"
              title="Editar Perfil"
            >
              <IoPencil className="h-5 w-5" />
            </Link>
          )}
        </div>
        <div className="absolute -bottom-16 -right-16 h-48 w-48 rounded-full bg-white/10 blur-3xl"></div>
        <div className="absolute -top-10 -left-10 h-32 w-32 rounded-full bg-black/10 blur-2xl"></div>
      </div>

      <div className="relative px-8 pb-10 pt-16">
        <div className="absolute -top-20 left-8 flex h-32 w-32 items-center justify-center rounded-2xl border-4 border-white bg-gray-100 text-5xl font-bold text-blue-600 shadow-md">
          {profile.name.charAt(0)}
        </div>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
          <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
            {/* Campo: Nombre */}
            <div className="sm:col-span-2">
              <label
                htmlFor={`${formId}-name`}
                className="text-xs font-bold uppercase tracking-widest text-gray-400"
              >
                Estudiante
              </label>
              {isEditing ? (
                <div>
                  <input
                    id={`${formId}-name`}
                    {...register("name")}
                    className="w-full rounded-md border-b-2 border-blue-500 bg-gray-50 py-1.5 text-2xl font-bold text-gray-900 focus:outline-none focus:bg-blue-50/50 transition-colors"
                    placeholder="Tu nombre completo"
                    autoFocus
                  />
                </div>
              ) : (
                <p className="text-3xl font-bold text-gray-900 leading-tight mt-1">
                  {profile.name}
                </p>
              )}
            </div>

            {/* Campo: Carrera (Columna Izquierda) */}
            <div>
              <label
                htmlFor={`${formId}-career`}
                className="text-xs font-bold uppercase tracking-widest text-gray-400"
              >
                Programa Académico
              </label>
              {isEditing ? (
                <input
                  id={`${formId}-career`}
                  {...register("career")}
                  className="mt-1 w-full rounded-md border-b-2 border-blue-500 bg-gray-50 py-1.5 text-base font-medium text-gray-700 focus:outline-none focus:bg-blue-50/50 transition-colors"
                  placeholder="Tu carrera"
                />
              ) : (
                <p className="text-lg font-medium text-blue-600 mt-1">
                  {profile.career}
                </p>
              )}
            </div>

            {/* Campo: Matrícula (Columna Derecha) */}
            <div>
              <label
                htmlFor={`${formId}-studentId`}
                className="text-xs font-bold uppercase tracking-widest text-gray-400"
              >
                Matrícula ID
              </label>
              {isEditing ? (
                <input
                  id={`${formId}-studentId`}
                  {...register("studentId")}
                  className="mt-1 w-full rounded-md border-b-2 border-blue-500 bg-gray-50 py-1.5 text-base font-medium text-gray-700 focus:outline-none focus:bg-blue-50/50 transition-colors"
                  placeholder="Tu número de matrícula"
                />
              ) : (
                <p className="font-mono text-lg tracking-widest text-gray-700 mt-1">
                  {profile.studentId || "2025-1122"}
                </p>
              )}
            </div>
          </div>

          {!isEditing && (
            <div className="mt-10 flex justify-between items-end border-t border-gray-100 pt-8">
              <IoBarcodeOutline className="h-16 w-3/4 text-gray-300" />
              <div className="h-16 w-16 rounded-full border-2 border-dashed border-gray-200 flex items-center justify-center opacity-50">
                <span className="text-[10px] font-bold text-gray-400 uppercase">
                  ITLA
                </span>
              </div>
            </div>
          )}

          {isEditing && (
            <div className="mt-10 flex justify-end gap-3 pt-6 border-t border-gray-100">
              <button
                type="button"
                onClick={handleCancel}
                disabled={isUpdatingProfile}
                className="flex items-center justify-center gap-2 rounded-lg bg-gray-100 px-6 py-2.5 text-sm font-semibold text-gray-600 transition-colors hover:bg-gray-200 hover:cursor-pointer disabled:opacity-50"
              >
                <IoClose className="h-5 w-5" /> Cancelar
              </button>
              <button
                type="submit"
                disabled={isUpdatingProfile}
                className="flex items-center justify-center gap-2 rounded-lg bg-blue-600 px-8 py-2.5 text-sm font-bold text-white shadow-md transition-colors hover:bg-blue-700 hover:cursor-pointer disabled:opacity-50"
              >
                {isUpdatingProfile ? (
                  "Guardando..."
                ) : (
                  <>
                    <IoCheckmark className="h-5 w-5" /> Guardar
                  </>
                )}
              </button>
            </div>
          )}
        </form>
      </div>
    </div>
  );
}
