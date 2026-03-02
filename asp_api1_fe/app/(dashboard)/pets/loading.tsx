export default function LoadingPets() {
  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
        <div>
          <div className="h-9 w-48 animate-pulse rounded-md bg-gray-200"></div>
          <div className="mt-2 h-5 w-80 animate-pulse rounded-md bg-gray-100"></div>
        </div>
      </div>

      <div className="h-80 w-full animate-pulse rounded-xl border border-gray-200 bg-white shadow-sm overflow-hidden">
        <div className="h-16 border-b border-gray-200 bg-gray-50 flex justify-end items-center px-4">
          <div className="h-9 w-36 bg-gray-200 rounded-lg"></div>
        </div>
        <div className="divide-y divide-gray-100">
          <div className="h-16 w-full bg-gray-50/50"></div>
          <div className="h-16 w-full bg-gray-50/50"></div>
          <div className="h-16 w-full bg-gray-50/50"></div>
        </div>
      </div>
    </div>
  );
}
