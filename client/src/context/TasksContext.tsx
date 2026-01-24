import React, { createContext, useContext, useState, useCallback } from "react";
import { tasksApi } from "../api/tasksApi";
import type { TaskResponse } from "../dto/responses/TaskResponse";
import type { CreateTaskRequest } from "../dto/requests/tasks/CreateTaskRequest";
import type { UpdateTaskRequest } from "../dto/requests/tasks/UpdateTaskRequest";
import { useError } from "./ErrorContext";

interface TasksContextType {
  tasks: TaskResponse[];
  isLoading: boolean;
  fetchTasks: (listId: number) => Promise<void>;

  updateTask: (updatedTask: UpdateTaskRequest) => void;
  addTask: (data: CreateTaskRequest) => void;
  deleteTask: (taskId: number) => void;
}

const TasksContext = createContext<TasksContextType | undefined>(undefined);

export const TasksProvider = ({ children }: { children: React.ReactNode }) => {
  const [tasks, setTasks] = useState<TaskResponse[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const { showError } = useError();

  const fetchTasks = useCallback(async (listId: number) => {
    if (listId === 0) return;
    setIsLoading(true);
    try {
      const data = await tasksApi.getAll(listId);
      setTasks(data);
    } catch (err) {
      console.error(err);
      showError("Unable to load tasks");
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
      showError("Error updating task");
    }
  };

  const addTask = async (data: CreateTaskRequest) => {
    try {
      const newTask = await tasksApi.create(data);
      setTasks((prev) => [...prev, newTask]);
    } catch (err) {
      console.error(err);
      showError("Error creating task");
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
      showError("Error deleting task");
    }
  };

  return (
    <TasksContext.Provider
      value={{
        tasks,
        isLoading,
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
