import { useRouter } from "next/navigation";
import { useCreatePet } from "@/hooks/pets";
import { useQueryString } from "@/hooks/shared/useQueryString";
import Modal from "@/components/shared/Modal";
import PetForm from "../PetForm";

export default function CreatePetModal() {
  // Hook personalizado para manejar la generación de URLs con query strings en Next.js
  const { createUrl, searchParams } = useQueryString();

  // Extraemos el router para controlar la apertura del modal
  const router = useRouter();

  // Extraemos el valor de la accion de la url
  const action = searchParams.get("action");

  // El modal de creación de gasto solo se muestra si la acción en los parámetros de búsqueda es "create-pet"
  const isOpen = action === "create-pet";

  // Función para cerrar el modal, que simplemente elimina los parámetros de la URL para cerrar el modal
  const closeModal = () => {
    const newUrl = createUrl({
      action: null,
    });
    router.replace(newUrl, { scroll: false });
  };

  // Usamos el hook de mutación para crear una nueva mascota
  const { mutate: createPet, isPending: isCreatingPet } = useCreatePet();

  return (
    <Modal title="Registrar Nueva Mascota" open={isOpen} close={closeModal}>
      <PetForm
        onSubmit={(data) => {
          createPet(data, {
            onSuccess: () => {
              closeModal();
            },
          });
        }}
        isLoading={isCreatingPet}
      />
    </Modal>
  );
}
