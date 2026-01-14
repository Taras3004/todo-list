import { useEffect, useState } from "react";
import type { TaskTagResponse } from "../dto/responses/TaskTagResponse";
import { tagsApi } from "../api/tagsApi";
import type { CreateTaskTagRequest } from "../dto/requests/taskTags/CreateTaskTagRequest";

export const useTags = () => {
  const [tags, setTags] = useState<TaskTagResponse[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchTasks = async () => {
    try {
      setIsLoading(true);
      const data = await tagsApi.getAll();
      setTags(data);
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

  const createTag = async (data: CreateTaskTagRequest) => {
    try {
      const newList = await tagsApi.create(data);
      setTags((prev) => [...prev, newList]);
    } catch (err) {
      console.error(err);
      alert("Error creating task");
    }
  };

  const deleteTag = async (id: number) => {
    try {
      await tagsApi.delete(id);
      setTags((prev) => prev.filter((t) => t.id !== id));
    } catch (err) {
      console.error(err);
    }
  };

  return {
    tags,
    isLoading,
    error,
    createTag,
    deleteTag,
  };
};
