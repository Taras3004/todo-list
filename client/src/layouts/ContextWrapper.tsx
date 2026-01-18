import { Outlet } from "react-router-dom";
import { TagsProvider } from "../context/TagsContext";
import { TasksProvider } from "../context/TasksContext";
import { ListsProvider } from "../context/ListsContext";
import { ErrorBoundary } from "react-error-boundary";
import { ErrorProvider } from "../context/ErrorContext";
import { ErrorToast } from "../components/ErrorToast";

const CriticalCrashFallback = () => (
  <div className="h-screen flex items-center justify-center bg-red-50">
    <h1>Critical error! Please, reload the page</h1>
  </div>
);

export const ContextWrapper = () => {
  return (
    <ErrorBoundary FallbackComponent={CriticalCrashFallback}>
      <ErrorProvider>
        <TasksProvider>
          <TagsProvider>
            <ListsProvider>
              <ErrorToast />
              <Outlet />
            </ListsProvider>
          </TagsProvider>
        </TasksProvider>
      </ErrorProvider>
    </ErrorBoundary>
  );
};
