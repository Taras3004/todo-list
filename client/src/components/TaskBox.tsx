import { Button } from "./Button";
import { Square, SquareCheck, Trash2 } from "lucide-react";

interface TaskBoxProps {
  name: string;
  deadline: string | Date;
  isCompleted?: boolean;
  onClick: () => void;
  onDeleteClicked: (e: React.MouseEvent) => void;
  onCheckBoxClicked: () => void;
}

export const TaskBox = ({
  name,
  onClick,
  deadline,
  isCompleted = false,
  onDeleteClicked,
  onCheckBoxClicked,
}: TaskBoxProps) => {
  const currentDate = new Date();
  const deadlineDate = new Date(deadline);

  const handleCheckBoxClick = (e: React.MouseEvent) => {
    e.stopPropagation();
    onCheckBoxClicked();
  };

  return (
    <Button onClick={onClick} className="w-full text-left mb-2 shadow">
      <div className="grid grid-cols-[30px_max-content_1fr_30px]">
        <div className="text-foreground" onClick={handleCheckBoxClick}>
          {isCompleted ? <SquareCheck /> : <Square />}
        </div>
        <h1
          className={`truncate font-semibold ml-2 ${
            isCompleted && "line-through"
          } ${deadlineDate < currentDate && !isCompleted && "text-red-600"}`}
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
