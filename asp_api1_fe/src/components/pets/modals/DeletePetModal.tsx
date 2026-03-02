import Modal from "@/components/shared/Modal";
import { usePet } from "@/hooks/pets";
import { useDeletePet } from "@/hooks/pets/useMutations";
import { useQueryString } from "@/hooks/shared/useQueryString";
import { useRouter } from "next/navigation";
import { IoWarning } from "react-icons/io5";

export default function DeletePetModal() {
  // Hook personalizado para manejar la generación de URLs con query strings en Next.js
  const { createUrl, searchParams } = useQueryString();

  // Extraemos el router para controlar la apertura del modal
  const router = useRouter();
  // Extraemos el valor de la accion de la url y el id de la mascota a modificar
  const action = searchParams.get("action");
  const petId = searchParams.get("petId")
    ? Number(searchParams.get("petId"))
    : 0;
  // Determinamos si el modal de edición debe estar abierto,
  // solo si la acción es "delete-pet" y tenemos un ID de mascota válido
  const openModal = !!(action === "delete-pet" && petId);

  // Usamos el hook de mutación para crear un nuevo gasto
  const { mutate: deletePet, isPending: isDeletingPet } = useDeletePet();

  // Función para manejar la eliminación del gasto,
  // que llama a la mutación de eliminación
  const handleDelete = () => {
    if (!petId) return;
    deletePet(petId, {
      onSuccess: () => {
        closeModal();
      },
    });
  };

  // Función para cerrar el modal, que simplemente elimina los parámetros de la URL para cerrar el modal
  const closeModal = () => {
    const newUrl = createUrl({
      action: null,
      expenseId: null,
    });
    router.replace(newUrl, { scroll: false });
  };

  // Usamos el hook de consulta para obtener los datos del gasto a editar
  const { data: petToDelete, isLoading: isLoadingPet } = usePet(
    openModal ? petId : 0,
  );

  return (
    <Modal
      title="Confirmar Eliminación"
      open={openModal}
      close={closeModal}
      size="small"
    >
      {isLoadingPet ? (
        <div className="py-10 text-center text-gray-500">Cargando...</div>
      ) : (
        <div className="space-y-6">
          <div className="flex items-center gap-4 rounded-lg bg-red-50 p-4 text-red-600">
            <IoWarning className="h-8 w-8 shrink-0" />
            <p className="text-sm font-medium">
              ¿Estás seguro de que deseas eliminar la mascota{" "}
              <span className="font-bold underline">
                &quot;{petToDelete?.name ?? "Cargando..."}&quot;
              </span>{" "}
              ? Esta acción no se puede deshacer.
            </p>
          </div>

          <div className="flex flex-col gap-3 sm:flex-row sm:justify-end">
            <button
              onClick={closeModal}
              className="rounded-lg px-4 py-2 text-sm font-semibold text-gray-900 hover:bg-gray-100 hover:cursor-pointer transition-colors"
            >
              Cancelar
            </button>
            <button
              onClick={handleDelete}
              disabled={isDeletingPet}
              className="rounded-lg bg-red-600 px-4 py-2 text-sm font-bold text-white shadow-sm hover:bg-red-700 hover:cursor-pointer transition-all disabled:opacity-50"
            >
              {isDeletingPet ? "Eliminando mascota..." : "Sí, eliminar mascota"}
            </button>
          </div>
        </div>
      )}
    </Modal>
  );
}
