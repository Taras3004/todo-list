import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { MainLayout } from "./layouts/MainLayout";
import { TasksPage } from "./pages/TasksPage";
import { LoginPage } from "./pages/LoginPage";
import { TaskDetails } from "./pages/TaskDetails";
import { ContextWrapper } from "./layouts/ContextWrapper";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<ContextWrapper />}>
          <Route path="/" element={<MainLayout />}>
            <Route index element={<Navigate to="/tasks" replace />} />

            <Route path="tasks" element={<TasksPage />}>
              <Route path=":id" element={<TaskDetails />} />
            </Route>

            <Route path="login" element={<LoginPage />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
