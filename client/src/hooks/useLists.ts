import { useEffect, useState } from "react";
import { listsApi } from "../api/listsApi";
import type { TodoListResponse } from "../dto/responses/TodoListResponse";
import type { CreateListRequest } from "../dto/requests/lists/CreateListRequest";

export const useLists = () => {
  const [lists, setLists] = useState<TodoListResponse[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchTasks = async () => {
    try {
      setIsLoading(true);
      const data = await listsApi.getAll();
      setLists(data);
    } catch (err) {
      console.error(err);
      setError("Error loading tasks");
    } finally {
      setIsLoading(false);
    }
  };
  
  useEffect(() => {
    fetchTasks();
  }, []);

  const createList = async (data: CreateListRequest) => {
    try {
      const newList = await listsApi.create(data);
      setLists((prev) => [...prev, newList]);
    } catch (err) {
      console.error(err);
      alert("Error creating task");
    }
  };

  const deleteList = async (id: number) => {
    try {
      await listsApi.delete(id);
      setLists((prev) => prev.filter((t) => t.id !== id));
    } catch (err) {
      console.error(err);
    }
  };

  return {
    lists,
    isLoading,
    error,
    createList,
    deleteList,
  };
};
