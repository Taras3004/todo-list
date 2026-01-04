import { Button } from "./Button";
import { useState } from "react";
import { Square } from "lucide-react";
import { SquareCheck } from "lucide-react";

interface TaskBoxProps {
  isCompleted?: boolean;
  name: string;
}

const TaskBox = ({ isCompleted = false, name }: TaskBoxProps) => {
  const [taskCompleted, setTaskCompleted] = useState(isCompleted);

  return (
    <Button onClick={() => console.log("clicked")}>
      <div className="grid grid-cols-[30px_1fr]">
        <button
          className="text-gray-700 hover:rotate-12"
          onClick={() => setTaskCompleted(!taskCompleted)}
        >
          {taskCompleted ? <SquareCheck /> : <Square />}
        </button>
        <h1
          className={`truncate font-semibold ml-2 ${
            taskCompleted && "line-through"
          }`}
        >
          {name}
        </h1>
        <div></div>
      </div>
    </Button>
  );
};

export default TaskBox;
