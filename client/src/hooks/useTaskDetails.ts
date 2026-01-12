import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { tasksApi } from "../api/tasksApi";
import type { TaskResponse } from "../dto/responses/TaskResponse";

export const useTaskDetails = () => {
  const { id } = useParams<{ id: string }>();
  const [task, setTask] = useState<TaskResponse | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTask = async () => {
      if (!id) return;

      try {
        setIsLoading(true);
        const data = await tasksApi.get(Number(id));
        setTask(data);
      } catch (err) {
        console.error(err);
        setError("Task not found");
      } finally {
        setIsLoading(false);
      }
    };

    fetchTask();
  }, [id]);

  return { task, isLoading, error };
};
