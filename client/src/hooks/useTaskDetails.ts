import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { tasksApi } from "../api/tasksApi";
import type { TaskDetailsResponse } from "../dto/responses/TaskDetailsResponse";

export const useTaskDetails = () => {
  const { id } = useParams<{ id: string }>();
  const [task, setTask] = useState<TaskDetailsResponse | null>(null);
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
        setError("Error loading task");
      } finally {
        setIsLoading(false);
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
  }: TaskDetailsResponse) => {
    try {
      setIsLoading(true);
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
    } finally {
      setIsLoading(false);
    }
  };

  return { task, isLoading, error, updateTaskDetails };
};
