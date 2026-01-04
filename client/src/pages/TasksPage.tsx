import TaskBox from "../Components/TaskBox";
import { ClipboardCheck } from "lucide-react";

export const TasksPage = () => {
  const tasks = [
    "task 1",
    "task 2fdjhdskfhskfheoufhesoufheofheio",
    "task 3",
    "task 4",
    "task 1283971983",
  ];

  return (
    <div>
      <div className="flex justify-center items-center mb-5 gap-2">
        <ClipboardCheck size={28} />
        <h1 className="font-bold text-4xl">List</h1>
      </div>

      <div className="flex-col justify-center">
        {tasks.map((task) => (
          <TaskBox name={task} />
        ))}
      </div>
    </div>
  );
};
