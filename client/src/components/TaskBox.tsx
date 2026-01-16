import { Button } from "./Button";
import { useState } from "react";
import { Square, SquareCheck, Trash2 } from "lucide-react";

interface TaskBoxProps {
  name: string;
  deadline: string | Date;
  isCompleted?: boolean;
  onClick: () => void;
  onDeleteClicked: (e: React.MouseEvent) => void;
}

export const TaskBox = ({
  name,
  onClick,
  deadline,
  isCompleted = false,
  onDeleteClicked,
}: TaskBoxProps) => {
  const [taskCompleted, setTaskCompleted] = useState(isCompleted);
  const currentDate = new Date();
  const deadlineDate = new Date(deadline);

  const handleCheckBoxClick = (e: React.MouseEvent) => {
    e.stopPropagation();
    setTaskCompleted(!taskCompleted);
  };

  return (
    <Button onClick={onClick} className="w-full text-left mb-2 shadow">
      <div className="grid grid-cols-[30px_max-content_1fr_30px]">
        <div
          className="text-foreground hover:rotate-12"
          onClick={handleCheckBoxClick}
        >
          {taskCompleted ? <SquareCheck /> : <Square />}
        </div>
        <h1
          className={`truncate font-semibold ml-2 ${
            taskCompleted && "line-through"
          } ${deadlineDate < currentDate && !taskCompleted && "text-red-600"}`}
        >
          {name}
        </h1>
        <p className="ml-1 text-gray-600">
          due to {deadlineDate.toLocaleString()}
        </p>
        <div onClick={onDeleteClicked}>
          <Trash2 />
        </div>
      </div>
    </Button>
  );
};
