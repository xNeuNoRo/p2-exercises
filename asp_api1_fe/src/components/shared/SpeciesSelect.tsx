"use client";

import { usePetSpeciesList } from "@/hooks/pets";
import { IoPrism } from "react-icons/io5";
import ErrorMessage from "./ErrorMessage";
import { useId } from "react";

type SpeciesSelectProps = {
  value?: string;
  onChange: (value: string) => void;
  error?: string;
  className?: string;
};

export default function SpeciesSelect({
  value,
  onChange,
  error,
  className = "",
}: Readonly<SpeciesSelectProps>) {
  // Consumimos el hook para obtener las especies de mascotas.
  const { data: species, isLoading } = usePetSpeciesList();

  // Generamos un ID único para el select y su label, asegurando accesibilidad y asociación correcta.
  const speciesSelectId = useId();

  // Si el valor es undefined, lo convertimos a string vacío para evitar problemas con el select
  const speciesValue = value ?? "";

  return (
    <div className={`flex flex-col gap-2 ${className}`}>
      <label
        htmlFor={speciesSelectId}
        className="text-sm font-semibold text-gray-900"
      >
        Especie
      </label>

      <div className="relative">
        <div className="absolute inset-y-0 left-0 flex items-center pl-3 text-gray-500">
          <IoPrism className="h-4 w-4" />
        </div>

        <select
          id={speciesSelectId}
          value={speciesValue}
          onChange={(e) => onChange(e.target.value)}
          disabled={isLoading}
          className={`
            block w-full rounded-lg border bg-gray-50 py-2.5 pl-10 pr-3 text-sm text-gray-900 transition-all hover:cursor-pointer
            focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500
            disabled:opacity-50
            ${error ? "border-red-500" : "border-gray-300"}
          `}
        >
          <option value="">
            {isLoading ? "Cargando especies..." : "Selecciona una especie"}
          </option>
          {species?.map((specie) => (
            <option key={specie.id} value={specie.id}>
              {specie.name}
            </option>
          ))}
        </select>
      </div>

      {/* Uso del componente reutilizable */}
      {error && <ErrorMessage>{error}</ErrorMessage>}
    </div>
  );
}
