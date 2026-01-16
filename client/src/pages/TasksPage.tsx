import { TaskBox } from "../components/TaskBox";
import { ClipboardCheck } from "lucide-react";
import { Outlet, useNavigate, useSearchParams } from "react-router-dom";
import { Button } from "../components/Button";
import { useEffect, useState } from "react";
import { useTasks } from "../hooks/useTasks";
import { listsApi } from "../api/listsApi";
import { CircleLoader } from "../components/CircleLoader";

export const TasksPage = () => {
  const [searchParams] = useSearchParams();

  const initialNameParam = searchParams.get("name") || "";
  const listIdParam = searchParams.get("todoListId") || "";
  const navigate = useNavigate();

  const todoListId = Number(listIdParam);
  const [newTask, setNewTask] = useState("");
  const { tasks, isLoading, error, createTask, deleteTask } =
    useTasks(todoListId);

  const [listName, setListName] = useState<string>("");

  useEffect(() => {
    const fetchListName = async () => {
      try {
        const response = await listsApi.get(todoListId);
        setListName(response.name);
      } catch (e) {
        console.error(e);
      }
    };

    fetchListName();
  }, [todoListId]);

  let filteredTasks = tasks;

  const handleTaskCreation = (e: React.FormEvent) => {
    e.preventDefault();

    if (newTask.trim() === "") return;
    createTask({ name: newTask, todoListId });
    setNewTask("");
  };

  const applyFilterIfExist = () => {
    if (initialNameParam.trim().length !== 0) {
      filteredTasks = tasks.filter((x) =>
        x.name.toLowerCase().includes(initialNameParam)
      );
    }
  };

  applyFilterIfExist();
  useEffect(applyFilterIfExist, [tasks]);

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
              isCompleted={false}
              onClick={() =>
                navigate(`${task.id.toString()}?${searchParams.toString()}`)
              }
              deadline={task.deadline}
              onDeleteClicked={(e: React.MouseEvent) => {
                e.stopPropagation();
                deleteTask(task.id);
              }}
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
