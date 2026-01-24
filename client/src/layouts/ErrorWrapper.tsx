import { ErrorBoundary } from "react-error-boundary";
import { ErrorProvider } from "../context/ErrorContext";
import { ErrorToast } from "../components/ErrorToast";
import { Outlet } from "react-router-dom";

const CriticalCrashFallback = () => (
  <div className="h-screen flex items-center justify-center bg-red-50">
    <h1>Critical error! Please, reload the page</h1>
  </div>
);

export const ErrorWrapper = () => {
  return (
    <ErrorBoundary FallbackComponent={CriticalCrashFallback}>
      <ErrorProvider>
        <ErrorToast />
        <Outlet />
      </ErrorProvider>
    </ErrorBoundary>
  );
};
