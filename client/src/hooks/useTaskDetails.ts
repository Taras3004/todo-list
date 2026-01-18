import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { tasksApi } from "../api/tasksApi";
import type { TaskDetailsResponse } from "../dto/responses/TaskDetailsResponse";
import type { UpdateTaskRequest } from "../dto/requests/tasks/UpdateTaskRequest";
import type { AddTaskTagRequest } from "../dto/requests/tasks/AddTaskTagRequest";
import type { TaskTagResponse } from "../dto/responses/TaskTagResponse";
import { useError } from "../context/ErrorContext";

export const useTaskDetails = () => {
  const { id } = useParams<{ id: string }>();
  const [task, setTask] = useState<TaskDetailsResponse | null>(null);
  const [tags, setTags] = useState<TaskTagResponse[] | null>(null);

  const { showError } = useError();

  const fetchTask = async () => {
    if (!id) return;

    try {
      const data = await tasksApi.get(Number(id));
      setTask(data);
      setTags(data.taskTags);
    } catch (err) {
      console.error(err);
      showError("Error loading task");
    }
  };

  useEffect(() => {
    fetchTask();
  }, [id]);

  const addTag = async ({ tagId }: AddTaskTagRequest) => {
    if (!task) return;
    try {
      await tasksApi.addTag(task.id, { tagId });
      fetchTask();
    } catch (err) {
      console.log(err);
      showError("Error adding tag");
    }
  };

  const removeTag = async (tagId: number) => {
    if (!task || !tags) return;
    try {
      await tasksApi.removeTag(task.id, tagId);
      fetchTask();
    } catch (err) {
      console.log(err);
      showError("Error removing tag");
    }
  };

  const updateTaskDetails = async ({
    id,
    name,
    deadline,
    isCompleted,
    description,
  }: UpdateTaskRequest) => {
    try {
      await tasksApi.update({
        id: id,
        name: name,
        deadline: deadline,
        isCompleted: isCompleted,
        description: description,
      });
    } catch (err) {
      console.error(err);
      showError("Error updating task");
    }
  };

  return { task, tags, addTag, removeTag, updateTaskDetails };
};
