import React, { createContext, useContext, useState, useCallback } from "react";
import { tasksApi } from "../api/tasksApi";
import type { TaskResponse } from "../dto/responses/TaskResponse";
import type { CreateTaskRequest } from "../dto/requests/tasks/CreateTaskRequest";
import type { UpdateTaskRequest } from "../dto/requests/tasks/UpdateTaskRequest";

interface TasksContextType {
  tasks: TaskResponse[];
  isLoading: boolean;
  error: string | null;
  fetchTasks: (listId: number) => Promise<void>;

  updateTask: (updatedTask: UpdateTaskRequest) => void;
  addTask: (data: CreateTaskRequest) => void;
  deleteTask: (taskId: number) => void;
}

const TasksContext = createContext<TasksContextType | undefined>(undefined);

export const TasksProvider = ({ children }: { children: React.ReactNode }) => {
  const [tasks, setTasks] = useState<TaskResponse[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchTasks = useCallback(async (listId: number) => {
    setIsLoading(true);
    try {
      const data = await tasksApi.getAll(listId);
      setTasks(data);
      setError(null);
    } catch (err) {
      console.error(err);
      setError("Unable to load tasks");
    } finally {
      setIsLoading(false);
    }
  }, []);

  const updateTask = async (data: UpdateTaskRequest) => {
    const oldTask = tasks.find((t) => t.id === data.id);
    if (!oldTask) return;
    const updatedTask: TaskResponse = {
      ...data,
      todoListId: oldTask.todoListId,
    };
    try {
      setTasks((prev) =>
        prev.map((t) => (t.id === updatedTask.id ? updatedTask : t)),
      );
      await tasksApi.update(data);
    } catch (err) {
      console.error(err);
      setTasks((prev) => prev.map((t) => (t.id === oldTask.id ? oldTask : t)));
      setError("Error updating task");
    }
  };

  const addTask = async (data: CreateTaskRequest) => {
    try {
      const newTask = await tasksApi.create(data);
      setTasks((prev) => [...prev, newTask]);
    } catch (err) {
      console.error(err);
      alert("Error creating task");
    }
  };

  const deleteTask = async (taskId: number) => {
    const deletedTask = tasks.find((t) => t.id === taskId);
    try {
      await tasksApi.delete(taskId);
      setTasks((prev) => prev.filter((t) => t.id !== taskId));
    } catch (err) {
      console.error(err);
      setTasks((prev) => {
        return { ...prev, deletedTask };
      });
      setError("Error deleting task");
    }
  };

  return (
    <TasksContext.Provider
      value={{
        tasks,
        isLoading,
        error,
        fetchTasks,
        updateTask,
        addTask,
        deleteTask,
      }}
    >
      {children}
    </TasksContext.Provider>
  );
};

export const useTasksContext = () => {
  const context = useContext(TasksContext);
  if (!context) {
    throw new Error("useTasksContext must be used within a TasksProvider");
  }
  return context;
};
