import { Outlet } from "react-router-dom";
import { TagsProvider } from "../context/TagsContext";
import { TasksProvider } from "../context/TasksContext";
import { ListsProvider } from "../context/ListsContext";

export const ContextWrapper = () => {
  return (
    <TasksProvider>
      <TagsProvider>
        <ListsProvider>
          <Outlet />
        </ListsProvider>
      </TagsProvider>
    </TasksProvider>
  );
};
