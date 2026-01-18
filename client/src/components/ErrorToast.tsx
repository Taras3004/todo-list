import { useError } from "../context/ErrorContext";

export const ErrorToast = () => {
  const { error } = useError();

  if (!error) return null;

  return (
    <div className="fixed z-100 right-[1%] bottom-5 transform bg-red-500 text-foreground px-6 py-3 rounded-default shadow-default">
      <p>{error}</p>
    </div>
  );
};
