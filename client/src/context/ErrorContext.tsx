import React, {
  createContext,
  useContext,
  useState,
  useCallback,
  useRef,
} from "react";

interface ErrorContextType {
  error: string | null;
  showError: (message: string) => void;
}

const ErrorContext = createContext<ErrorContextType | undefined>(undefined);

export const ErrorProvider = ({ children }: { children: React.ReactNode }) => {
  const [error, setError] = useState<string | null>(null);
  const timeoutRef = useRef<number | null>(null);

  const showError = useCallback((message: string) => {
    setError(message);

    if (timeoutRef.current) clearTimeout(timeoutRef.current);

    timeoutRef.current = window.setTimeout(() => {
      setError(null);
    }, 3000);
  }, []);

  return (
    <ErrorContext.Provider value={{ error, showError }}>
      {children}
    </ErrorContext.Provider>
  );
};

export const useError = () => {
  const context = useContext(ErrorContext);
  if (!context) throw new Error("useError must be used within ErrorProvider");
  return context;
};
