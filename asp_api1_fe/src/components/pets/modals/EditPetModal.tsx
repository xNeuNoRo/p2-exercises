import Modal from "@/components/shared/Modal";
import PetForm from "../PetForm";
import { useQueryString } from "@/hooks/shared/useQueryString";
import { useRouter } from "next/navigation";
import { usePet, useUpdatePet } from "@/hooks/pets";

export default function EditPetModal() {
  // Hook personalizado para manejar la generación de URLs con query strings en Next.js
  const { createUrl, searchParams } = useQueryString();

  // Extraemos el router para controlar la apertura del modal
  const router = useRouter();
  // Extraemos el valor de la accion de la url y el id de la mascosta a modificar
  const action = searchParams.get("action");
  const petId = searchParams.get("petId")
    ? Number(searchParams.get("petId"))
    : null;
  // Determinamos si el modal de edición debe estar abierto,
  // solo si la acción es "edit-pet" y tenemos un ID de mascota válido
  const openModal = !!(action === "edit-pet" && petId);

  // Usamos el hook de mutación para crear un nuevo gasto
  const { mutate: updatePet, isPending: isUpdatingPet } = useUpdatePet();

  // Función para cerrar el modal, que simplemente elimina los parámetros de la URL para cerrar el modal
  const closeModal = () => {
    const newUrl = createUrl({
      action: null,
      petId: null,
    });
    router.replace(newUrl, { scroll: false });
  };

  // Usamos el hook de consulta para obtener los datos del gasto a editar
  const { data: petToEdit, isLoading: isLoadingPet } = usePet(
    openModal ? petId : 0,
  );

  return (
    <Modal title="Editar Mascota" open={openModal} close={closeModal}>
      {isLoadingPet ? (
        <div className="py-10 text-center text-gray-500">Cargando...</div>
      ) : (
        <PetForm
          initialData={petToEdit}
          onSubmit={(data) => {
            if (petId) {
              updatePet(
                { ...data, id: petId },
                {
                  onSuccess: () => {
                    closeModal();
                  },
                },
              );
            }
          }}
          isLoading={isUpdatingPet}
        />
      )}
    </Modal>
  );
}
