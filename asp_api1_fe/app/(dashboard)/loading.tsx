export default function LoadingProfile() {
  return (
    <div className="flex min-h-[calc(100vh-8rem)] items-center justify-center p-4">
      <div className="h-[28rem] w-full max-w-3xl animate-pulse rounded-2xl border border-gray-200 bg-gray-100/50 shadow-xl">
        <div className="h-40 w-full bg-gray-200/50 rounded-t-2xl"></div>
        <div className="p-8 pt-16 relative">
          <div className="absolute -top-20 left-8 h-32 w-32 rounded-2xl bg-gray-200 border-4 border-white"></div>
          <div className="space-y-6">
            <div className="h-10 w-3/4 bg-gray-200 rounded-lg"></div>
            <div className="grid grid-cols-2 gap-6">
              <div className="h-8 w-full bg-gray-200 rounded-lg"></div>
              <div className="h-8 w-full bg-gray-200 rounded-lg"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
