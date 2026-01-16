import { Modal } from "../components/Modal";
import { Tag, X, Calendar } from "lucide-react";
import { Button } from "../components/Button";
import TextareaAutosize from "react-textarea-autosize";
import { useTaskDetails } from "../hooks/useTaskDetails";
import { useEffect, useState } from "react";
import { CircleLoader } from "../components/CircleLoader";

export const TaskDetails = () => {
  const { task, error, updateTaskDetails } = useTaskDetails();
  const [description, setDescription] = useState<string>("");
  const tags = task?.taskTags || [];

  useEffect(() => {
    if (task) {
      setDescription(task.description || "");
    }
  }, [task]);

  const handleUpdatingTask = () => {
    updateTaskDetails({ ...task!, description });
  };

  return (
    <Modal
      title={`Task - ${task?.name ?? "Loading..."}`}
      onModalClosed={() => handleUpdatingTask()}
    >
      
      <div className="pl-6 pr-6 pb-6">
        <div className="mb-4 flex gap-1 items-center justify-start">
          <Calendar />
          <p className="text-base font-bold">Deadline</p>
          <p className="text-base">
            - {task ? new Date(task.deadline).toLocaleString() : "Loading..."}
          </p>
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
          {task ? (
            <TextareaAutosize
              className="text-foreground wrap-break-word text-left w-full outline-none resize-none"
              placeholder="Enter description here..."
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
          ) : (
            <CircleLoader />
          )}
        </div>
      </div>
    </Modal>
  );
};
