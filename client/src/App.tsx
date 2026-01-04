import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { MainLayout } from "./layouts/MainLayout";
import { TasksPage } from "./pages/TasksPage";
import { LoginPage } from "./pages/LoginPage";
import { TagsPage } from "./pages/TagsPage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Navigate to="/tasks" replace />} />

          <Route path="tasks" element={<TasksPage />} />
          <Route path="taskTags" element={<TagsPage />} />
          <Route path="login" element={<LoginPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
