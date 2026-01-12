import { useState, useEffect } from "react";
import { tasksApi } from "../api/tasksApi";
import type { TaskResponse } from "../dto/responses/TaskResponse";
import type { CreateTaskRequest } from "../dto/requests/tasks/CreateTaskRequest";

export const useTasks = (todoListId: number) => {
  const [tasks, setTasks] = useState<TaskResponse[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchTasks = async () => {
    try {
      setIsLoading(true);
      const data = await tasksApi.getAll(todoListId);
      setTasks(data);
    } catch (err) {
      console.error(err);
      setError("Error loading tasks");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, [todoListId]);

  const createTask = async (data: CreateTaskRequest) => {
    try {
      const newTask = await tasksApi.create(data);
      setTasks((prev) => [...prev, newTask]);
    } catch (err) {
      console.error(err);
      alert("Error creating task");
    }
  };

  const deleteTask = async (id: number) => {
    try {
      await tasksApi.delete(id);
      setTasks((prev) => prev.filter((t) => t.id !== id));
    } catch (err) {
      console.error(err);
    }
  };

  return {
    tasks,
    isLoading,
    error,
    createTask,
    deleteTask,
    refetch: fetchTasks,
  };
};
