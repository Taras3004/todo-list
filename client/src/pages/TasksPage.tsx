import { TaskBox } from "../components/TaskBox";
import { ClipboardCheck } from "lucide-react";
import { Outlet, useNavigate, useSearchParams } from "react-router-dom";
import { Button } from "../components/Button";
import { useEffect, useState } from "react";
import { useTasks } from "../hooks/useTasks";

export const TasksPage = () => {
  const [searchParams] = useSearchParams();

  const initialNameParam = searchParams.get("name") || "";
  const navigate = useNavigate();

  const [newTask, setNewTask] = useState("");
  const { tasks, isLoading, error, createTask, deleteTask } = useTasks(1);

  let filteredTasks = tasks;

  const handleTaskCreation = (e: React.FormEvent) => {
    e.preventDefault();

    if (newTask.trim() === "") return;
    createTask({ name: newTask, todoListId: 1 });
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

  return (
    <div>
      <div className="flex justify-center items-center mb-5 gap-2">
        <ClipboardCheck size={28} />
        <h1 className="font-bold text-4xl">List</h1>
      </div>

      <div className="flex-col justify-center">
        {filteredTasks.map((task) => (
          <TaskBox
            key={task.id}
            name={task.name}
            isCompleted={false}
            onClick={() => navigate(task.id.toString())}
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
    </div>
  );
};
