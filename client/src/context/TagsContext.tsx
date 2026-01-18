import { createContext, useContext, useEffect, useState } from "react";
import type { TaskTagResponse } from "../dto/responses/TaskTagResponse";
import type { CreateTaskTagRequest } from "../dto/requests/taskTags/CreateTaskTagRequest";
import { tagsApi } from "../api/tagsApi";
import { useError } from "./ErrorContext";

interface TagsContextType {
  tags: TaskTagResponse[];
  isLoading: boolean;
  fetchTags: () => Promise<void>;

  createTag: (data: CreateTaskTagRequest) => void;
  deleteTag: (tagId: number) => void;
}

const TagsContext = createContext<TagsContextType | undefined>(undefined);

export const TagsProvider = ({ children }: { children: React.ReactNode }) => {
  const [tags, setTags] = useState<TaskTagResponse[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  const { showError } = useError();

  const fetchTags = async () => {
    try {
      setIsLoading(true);
      const data = await tagsApi.getAll();
      setTags(data);
    } catch (err) {
      console.error(err);
      showError("Error loading tags");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchTags();
  }, []);

  const createTag = async (data: CreateTaskTagRequest) => {
    try {
      const newList = await tagsApi.create(data);
      setTags((prev) => [...prev, newList]);
    } catch (err) {
      console.error(err);
      showError("Error creating tag");
    }
  };

  const deleteTag = async (id: number) => {
    const deletedTag = tags.find((t) => t.id === id);
    try {
      setTags((prev) => prev.filter((t) => t.id !== id));
      await tagsApi.delete(id);
    } catch (err) {
      setTags((prev) => {
        return { ...prev, deletedTag };
      });
      console.error(err);
      showError("Error deleting tag");
    }
  };

  return (
    <TagsContext.Provider
      value={{ tags, isLoading, fetchTags, createTag, deleteTag }}
    >
      {children}
    </TagsContext.Provider>
  );
};

export const useTagsContext = () => {
  const context = useContext(TagsContext);
  if (!context) {
    throw new Error("useTagsContext must be used within a TagsProvider");
  }
  return context;
};
