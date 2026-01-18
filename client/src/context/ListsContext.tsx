import { createContext, useContext, useEffect, useState } from "react";
import type { TodoListResponse } from "../dto/responses/TodoListResponse";
import type { CreateListRequest } from "../dto/requests/lists/CreateListRequest";
import type { UpdateListRequest } from "../dto/requests/lists/UpdateListRequest";
import { listsApi } from "../api/listsApi";
import { useError } from "./ErrorContext";

interface ListsContextType {
  lists: TodoListResponse[];
  isLoading: boolean;
  fetchLists: () => Promise<void>;

  createList: (data: CreateListRequest) => void;
  updateList: (data: UpdateListRequest) => void;
  deleteList: (tagId: number) => void;
}

const ListsContext = createContext<ListsContextType | undefined>(undefined);

export const ListsProvider = ({ children }: { children: React.ReactNode }) => {
  const [lists, setLists] = useState<TodoListResponse[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { showError } = useError();

  const fetchLists = async () => {
    try {
      setIsLoading(true);
      const data = await listsApi.getAll();
      setLists(data);
    } catch (err) {
      console.error(err);
      showError("Error loading tasks");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchLists();
  }, []);

  const createList = async (data: CreateListRequest) => {
    try {
      const newList = await listsApi.create(data);
      setLists((prev) => [...prev, newList]);
    } catch (err) {
      console.error(err);
      showError("Error creating task");
    }
  };

  const updateList = async (data: UpdateListRequest) => {
    const oldList = lists.find((t) => t.id === data.id);
    if (!oldList) return;
    const updatedList: TodoListResponse = { ...data };
    try {
      setLists((prev) =>
        prev.map((t) => (t.id === updatedList.id ? updatedList : t)),
      );
      await listsApi.update(data);
    } catch (err) {
      console.error(err);
      setLists((prev) => prev.map((t) => (t.id === oldList.id ? oldList : t)));
      showError("Error updating task");
    }
  };

  const deleteList = async (id: number) => {
    const deletedList = lists.find((t) => t.id === id);
    try {
      setLists((prev) => prev.filter((t) => t.id !== id));
      await listsApi.delete(id);
    } catch (err) {
      setLists((prev) => {
        return { ...prev, deletedTag: deletedList };
      });
      console.error(err);
      showError("Error deleting list");
    }
  };

  return (
    <ListsContext.Provider
      value={{
        lists,
        isLoading,
        fetchLists,
        createList,
        updateList,
        deleteList,
      }}
    >
      {children}
    </ListsContext.Provider>
  );
};

export const useListsContext = () => {
  const context = useContext(ListsContext);
  if (!context) {
    throw new Error("useTagsContext must be used within a TagsProvider");
  }
  return context;
};
