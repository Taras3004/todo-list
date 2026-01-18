import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { tasksApi } from "../api/tasksApi";
import type { TaskDetailsResponse } from "../dto/responses/TaskDetailsResponse";
import type { UpdateTaskRequest } from "../dto/requests/tasks/UpdateTaskRequest";
import type { AddTaskTagRequest } from "../dto/requests/tasks/AddTaskTagRequest";
import type { TaskTagResponse } from "../dto/responses/TaskTagResponse";

export const useTaskDetails = () => {
  const { id } = useParams<{ id: string }>();
  const [task, setTask] = useState<TaskDetailsResponse | null>(null);
  const [tags, setTags] = useState<TaskTagResponse[] | null>(null);
  const [error, setError] = useState<string | null>(null);

  const fetchTask = async () => {
    if (!id) return;

    try {
      const data = await tasksApi.get(Number(id));
      setTask(data);
      setTags(data.taskTags);
    } catch (err) {
      console.error(err);
      setError("Error loading task");
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
      setError("Error adding tag");
    }
  };

  const removeTag = async (tagId: number) => {
    if (!task || !tags) return;
    try {
      await tasksApi.removeTag(task.id, tagId);
      fetchTask();
    } catch (err) {
      console.log(err);
      setError("Error removing tag");
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
      setError("Error updating task");
    }
  };

  return { task, error, tags, addTag, removeTag, updateTaskDetails };
};
