import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { tasksApi } from "../api/tasksApi";
import type { TaskDetailsResponse } from "../dto/responses/TaskDetailsResponse";
import type { UpdateTaskRequest } from "../dto/requests/tasks/UpdateTaskRequest";

export const useTaskDetails = () => {
  const { id } = useParams<{ id: string }>();
  const [task, setTask] = useState<TaskDetailsResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTask = async () => {
      if (!id) return;

      try {
        const data = await tasksApi.get(Number(id));
        setTask(data);
      } catch (err) {
        console.error(err);
        setError("Error loading task");
      }
    };

    fetchTask();
  }, [id]);

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

  return { task, error, updateTaskDetails };
};
