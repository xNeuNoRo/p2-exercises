"use client";

import { usePets } from "@/hooks/pets/useQueries";
import { IoAdd, IoPencil, IoTrash } from "react-icons/io5";
import { useQueryString } from "@/hooks/shared/useQueryString";
import CreatePetModal from "./modals/CreatePetModal";
import EditPetModal from "./modals/EditPetModal";
import DeletePetModal from "./modals/DeletePetModal";
import Link from "next/link";

export default function PetsClient() {
  const { createUrl } = useQueryString();
  const { data: pets, isLoading } = usePets();

  if (isLoading)
    return (
      <div className="h-64 animate-pulse rounded-xl bg-white border border-gray-200"></div>
    );

  return (
    <div className="rounded-xl border border-gray-200 bg-white shadow-sm overflow-hidden">
      <div className="p-4 border-b border-gray-200 flex justify-end bg-gray-50">
        <Link
          href={createUrl({
            action: "create-pet",
          })}
          className="flex items-center gap-2 rounded-lg bg-blue-600 px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-blue-700 shadow-sm"
        >
          <IoAdd className="h-5 w-5" />
          Nueva Mascota
        </Link>
      </div>

      <div className="overflow-x-auto">
        <table className="w-full text-left text-sm text-gray-600">
          <thead className="bg-white text-xs uppercase text-gray-500 border-b border-gray-200">
            <tr>
              <th className="px-6 py-4 font-semibold">Nombre</th>
              <th className="px-6 py-4 font-semibold">Especie</th>
              <th className="px-6 py-4 font-semibold">Raza</th>
              <th className="px-6 py-4 font-semibold">Edad</th>
              <th className="px-6 py-4 font-semibold text-center">Acciones</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-100">
            {pets?.length === 0 ? (
              <tr>
                <td
                  colSpan={5}
                  className="px-6 py-12 text-center text-gray-500"
                >
                  No tienes mascotas registradas. ¡Agrega una!
                </td>
              </tr>
            ) : (
              pets?.map((pet) => (
                <tr key={pet.id} className="hover:bg-gray-50 transition-colors">
                  <td className="px-6 py-4 font-medium text-gray-900">
                    {pet.name}
                  </td>
                  <td className="px-6 py-4">
                    <span className="inline-flex items-center rounded-full bg-blue-50 px-2 py-1 text-xs font-medium text-blue-700 ring-1 ring-inset ring-blue-700/10">
                      {pet.speciesName}
                    </span>
                  </td>
                  <td className="px-6 py-4">{pet.race}</td>
                  <td className="px-6 py-4">{pet.age} años</td>
                  <td className="px-6 py-4 text-center">
                    <div className="flex items-center justify-center gap-2">
                      <Link
                        href={createUrl({
                          action: "edit-pet",
                          petId: pet.id,
                        })}
                        className="p-2.5 text-gray-400 hover:text-blue-600 hover:bg-blue-50 rounded-full transition-colors"
                      >
                        <IoPencil className="h-5 w-5" />
                      </Link>
                      <Link
                        href={createUrl({
                          action: "delete-pet",
                          petId: pet.id,
                        })}
                        className="p-2.5 text-gray-400 hover:text-red-600 hover:bg-red-50 rounded-full transition-colors ml-1"
                      >
                        <IoTrash className="h-5 w-5" />
                      </Link>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <CreatePetModal />
      <EditPetModal />
      <DeletePetModal />
    </div>
  );
}
