"use client";

import { useId } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { CreatePetSchema, CreatePetFormData, Pet } from "@/schemas/pet";
import SpeciesSelect from "@/components/shared/SpeciesSelect";
import ErrorMessage from "@/components/shared/ErrorMessage";

type PetFormProps = {
  initialData?: Pet;
  onSubmit: (data: CreatePetFormData) => void;
  isLoading?: boolean;
};

export default function PetForm({
  initialData,
  onSubmit,
  isLoading,
}: Readonly<PetFormProps>) {
  // Generamos un ID único para el formulario, que se usará para asociar labels e inputs de manera accesible.
  const formId = useId();

  // Configuramos React Hook Form con el resolver de Zod para validación y los valores por defecto.
  const {
    register,
    handleSubmit,
    setValue,
    watch,
    formState: { errors },
  } = useForm<CreatePetFormData>({
    resolver: zodResolver(CreatePetSchema),
    defaultValues: initialData
      ? {
          name: initialData.name,
          race: initialData.race,
          age: initialData.age,
          species: initialData.speciesId,
        }
      : {
          name: "",
          race: "",
          age: 0,
          species: undefined,
        },
  });

  // Obtenemos el valor actual de species para pasarlo al SpeciesSelect
  // eslint-disable-next-line react-hooks/incompatible-library
  const speciesIdValue = watch("species")?.toString() || "";

  // Determinar el texto del boton de envio segun el estado del formulario
  let buttonText = "Registrar Mascota";
  if (isLoading) {
    buttonText = "Guardando...";
  } else if (initialData) {
    buttonText = "Actualizar Mascota";
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <label
          htmlFor={`${formId}-name`}
          className="block text-sm font-semibold text-gray-900 mb-1"
        >
          Nombre
        </label>
        <input
          id={`${formId}-name`}
          {...register("name")}
          type="text"
          className={`w-full rounded-lg border bg-gray-50 px-3 py-2 text-sm text-gray-900 focus:outline-none focus:ring-1 ${
            errors.name
              ? "border-red-500 focus:border-red-500 focus:ring-red-500"
              : "border-gray-300 focus:border-blue-500 focus:ring-blue-500"
          }`}
          placeholder="Ej: Firulais"
        />
        {errors.name && <ErrorMessage>{errors.name.message}</ErrorMessage>}
      </div>

      <div className="grid grid-cols-2 gap-4">
        {/* Integración del SpeciesSelect manual con RHF usando setValue y watch */}
        <SpeciesSelect
          value={speciesIdValue}
          onChange={(val) =>
            setValue("species", Number.parseInt(val), { shouldValidate: true })
          }
          error={errors.species?.message}
        />

        <div className="flex flex-col gap-2">
          <label
            htmlFor={`${formId}-race`}
            className="block text-sm font-semibold text-gray-900"
          >
            Raza
          </label>
          <input
            id={`${formId}-race`}
            {...register("race")}
            type="text"
            className={`w-full rounded-lg border bg-gray-50 px-3 py-2.5 text-sm text-gray-900 focus:outline-none focus:ring-1 ${
              errors.race
                ? "border-red-500 focus:border-red-500 focus:ring-red-500"
                : "border-gray-300 focus:border-blue-500 focus:ring-blue-500"
            }`}
            placeholder="Ej: Labrador"
          />
          {errors.race && <ErrorMessage>{errors.race.message}</ErrorMessage>}
        </div>
      </div>

      <div>
        <label
          htmlFor={`${formId}-age`}
          className="block text-sm font-semibold text-gray-900 mb-1"
        >
          Edad (Años)
        </label>
        <input
          id={`${formId}-age`}
          {...register("age", { valueAsNumber: true })}
          type="number"
          min="0"
          className={`w-full rounded-lg border bg-gray-50 px-3 py-2 text-sm text-gray-900 focus:outline-none focus:ring-1 ${
            errors.age
              ? "border-red-500 focus:border-red-500 focus:ring-red-500"
              : "border-gray-300 focus:border-blue-500 focus:ring-blue-500"
          }`}
        />
        {errors.age && <ErrorMessage>{errors.age.message}</ErrorMessage>}
      </div>

      <div className="mt-6 flex justify-end space-x-3 pt-4 border-t border-gray-100">
        <button
          type="submit"
          disabled={isLoading}
          className="rounded-lg bg-blue-600 px-5 py-2.5 text-sm font-medium text-white shadow-sm hover:bg-blue-700 hover:cursor-pointer disabled:opacity-50 transition-colors"
        >
          {buttonText}
        </button>
      </div>
    </form>
  );
}
