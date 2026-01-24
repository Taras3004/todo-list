import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { MainLayout } from "./layouts/MainLayout";
import { TasksPage } from "./pages/TasksPage";
import { LoginPage } from "./pages/LoginPage";
import { TaskDetails } from "./pages/TaskDetails";
import { ContentWrapper } from "./layouts/ContentWrapper";
import { ErrorWrapper } from "./layouts/ErrorWrapper";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<ErrorWrapper />}>
          <Route path="login" element={<LoginPage />} />
          <Route element={<ContentWrapper />}>
            <Route path="/" element={<MainLayout />}>
              <Route index element={<Navigate to="/tasks" replace />} />

              <Route path="tasks" element={<TasksPage />}>
                <Route path=":id" element={<TaskDetails />} />
              </Route>
            </Route>
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
