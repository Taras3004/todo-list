import { Modal } from "../components/Modal";
import { Tag, X, Calendar } from "lucide-react";
import { Button } from "../components/Button";
import TextareaAutosize from "react-textarea-autosize";
import { useTaskDetails } from "../hooks/useTaskDetails";
import { useEffect, useState } from "react";
import { CircleLoader } from "../components/CircleLoader";
import { Popup } from "../components/Popup";
import { useTasksContext } from "../context/TasksContext";
import type { TaskTagResponse } from "../dto/responses/TaskTagResponse";
import { useTagsContext } from "../context/TagsContext";
import { CheckBox } from "../components/Checkbox";

export const TaskDetails = () => {
  const { task, tags: taskTags, addTag, removeTag } = useTaskDetails();
  const { updateTask } = useTasksContext();
  const [description, setDescription] = useState<string>("");
  const [taskCompleted, setTaskCompleted] = useState(false);

  const { tags: allTags } = useTagsContext();

  const availableTags: TaskTagResponse[] = !taskTags
    ? []
    : allTags.filter((x) => !taskTags.some((y) => y.id === x.id));

  useEffect(() => {
    if (task) {
      setDescription(task.description || "");
      setTaskCompleted(task.isCompleted);
    }
  }, [task]);

  const saveDescription = () => {
    if (task && task.description !== description) {
      updateTask({ ...task, description });
    }
  };

  const toggleIsCompleted = () => {
    if (!task) return;

    const newValue: boolean = !taskCompleted;
    setTaskCompleted(newValue);
    {
      updateTask({ ...task, isCompleted: newValue });
    }
  };

  return (
    <Modal>
      <div className="flex justify-start items-center p-4">
        <h1 className="text-xl font-bold text-foreground">
          {`${task?.name ?? "Loading..."}`}
        </h1>
      </div>

      <div className="pl-6 pr-6 pb-6">
        <div className="mb-4 flex gap-1 items-center justify-start">
          <CheckBox isActive={taskCompleted} onClick={toggleIsCompleted} />
          <p className="text-base">
            {task && `- task is ${taskCompleted ? "" : "not"} completed`}
          </p>
        </div>

        <div className="mb-4 flex gap-1 items-center justify-start">
          <Calendar />
          <p className="text-base font-bold">Deadline</p>
          <p className="text-base">
            - {task ? new Date(task.deadline).toLocaleString() : "Loading..."}
          </p>
        </div>

        <div className="mb-4 flex gap-1 items-center justify-start">
          <Tag />
          <Popup trigger={<p className="text-base font-bold mr-1">Tags</p>}>
            {allTags && task && (
              <div className="flex flex-col overflow-y-auto max-h-20 pr-3.5 gap-2">
                {availableTags.map((availableTag) => (
                  <Button
                    className="p-2"
                    onClick={() => addTag({ tagId: availableTag.id })}
                  >
                    {availableTag.tag}
                  </Button>
                ))}
              </div>
            )}
          </Popup>

          <div className="text-base flex gap-2">
            {task &&
              taskTags &&
              taskTags.map((tag) => (
                <Button onClick={() => removeTag(tag.id)}>
                  <div className="flex items-center justify-center">
                    <p>{tag.tag}</p>
                    <X className="ml-1 mr-1" size={15} />
                  </div>
                </Button>
              ))}
          </div>
        </div>

        <div className="mb-4">
          <label className="text-foreground font-bold">Description</label>
          {task ? (
            <TextareaAutosize
              className="text-foreground wrap-break-word text-left w-full outline-none resize-none"
              placeholder="Enter description here..."
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              onBlur={saveDescription}
            />
          ) : (
            <CircleLoader />
          )}
        </div>
      </div>
    </Modal>
  );
};
