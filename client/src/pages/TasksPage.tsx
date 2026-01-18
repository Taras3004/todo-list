import { useEffect, useState } from "react";
import { Outlet, useNavigate, useSearchParams } from "react-router-dom";
import { useTasksContext } from "../context/TasksContext";
import { TaskBox } from "../components/TaskBox";
import { Button } from "../components/Button";
import { CircleLoader } from "../components/CircleLoader";
import { ClipboardCheck } from "lucide-react";
import { useListsContext } from "../context/ListsContext";

export const TasksPage = () => {
  const [searchParams] = useSearchParams();

  const initialNameParam = searchParams.get("name") || "";
  const listIdParam = searchParams.get("todoListId") || "";
  const navigate = useNavigate();

  const todoListId = Number(listIdParam);
  const [newTask, setNewTask] = useState("");

  const { tasks, isLoading, fetchTasks, addTask, updateTask, deleteTask } =
    useTasksContext();

  const { lists } = useListsContext();

  const currentList = lists.find((t) => t.id === todoListId);
  const listName = currentList?.name || "";

  useEffect(() => {
    fetchTasks(todoListId);
  }, [todoListId, fetchTasks]);

  let filteredTasks = tasks;

  const handleTaskCreation = (e: React.FormEvent) => {
    e.preventDefault();

    if (newTask.trim() === "") return;
    addTask({ name: newTask, todoListId });
    setNewTask("");
  };

  const applyFilterIfExist = () => {
    if (initialNameParam.trim().length !== 0) {
      filteredTasks = tasks.filter((x) =>
        x.name.toLowerCase().includes(initialNameParam),
      );
    }
  };

  applyFilterIfExist();

  if (todoListId === 0) {
    return (
      <div>
        <p>Select or create new list...</p>
      </div>
    );
  }

  return (
    <div>
      <div className="flex justify-center items-center mb-5 gap-2">
        <ClipboardCheck size={28} />
        <h1 className="font-bold text-4xl">
          {isLoading ? <CircleLoader /> : listName}
        </h1>
      </div>
      {isLoading ? (
        <CircleLoader />
      ) : (
        <div className="flex-col justify-center">
          {filteredTasks.map((task) => (
            <TaskBox
              key={task.id}
              name={task.name}
              isCompleted={task.isCompleted}
              onClick={() =>
                navigate(`${task.id.toString()}?${searchParams.toString()}`)
              }
              deadline={task.deadline}
              onDeleteClicked={(e: React.MouseEvent) => {
                e.stopPropagation();
                deleteTask(task.id);
              }}
              onCheckBoxClicked={() =>
                updateTask({ ...task, isCompleted: !task.isCompleted })
              }
            />
          ))}
          <Button className="w-full text-left">
            <form onSubmit={handleTaskCreation}>
              <input
                type="text"
                placeholder="new task..."
                className="outline-none ml-2 w-full"
                value={newTask}
                onChange={(e) => setNewTask(e.target.value)}
              ></input>
            </form>
          </Button>
          <Outlet />
        </div>
      )}
    </div>
  );
};
