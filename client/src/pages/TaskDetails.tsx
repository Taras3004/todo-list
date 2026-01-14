import { Modal } from "../components/Modal";
import { Tag, X, Calendar } from "lucide-react";
import { Button } from "../components/Button";
import TextareaAutosize from "react-textarea-autosize";
import { useTaskDetails } from "../hooks/useTaskDetails";

export const TaskDetails = () => {
  const { task, isLoading, error, updateTaskDetails } = useTaskDetails();
  const tags = task?.taskTags || [];

  if (task === null) return;

  const deadlineDate = new Date(task.deadline);
  return (
    <Modal title={`Task - ${task.name}`} onModalClosed={() => updateTaskDetails(task)}>
      <div className="pl-6 pr-6 pb-6">
        <div className="mb-4 flex gap-1 items-center justify-start">
          <Calendar />
          <p className="text-base font-bold">Deadline</p>
          <p className="text-base">- {deadlineDate.toLocaleString()}</p>
        </div>

        <div className="mb-4 flex gap-1 items-center justify-start">
          <Tag />
          <p className="text-base font-bold mr-1">Tags</p>
          <p className="text-base flex gap-2">
            {tags.map((tag) => (
              <Button>
                <div className="flex items-center justify-center">
                  <p>{tag.tag}</p>
                  <X className="ml-1 mr-1" size={15} />
                </div>
              </Button>
            ))}
          </p>
        </div>

        <div className="mb-4">
          <label className="text-foreground font-bold">Description</label>
          <TextareaAutosize
            className="text-foreground wrap-break-word text-left w-full outline-none resize-none"
            placeholder="Enter description here..."
          >
            {task.description}
          </TextareaAutosize>
        </div>
      </div>
    </Modal>
  );
};
