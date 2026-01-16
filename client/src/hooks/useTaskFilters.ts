import { useSearchParams } from "react-router-dom";
import type { TaskFilters } from "../interfaces/TaskFilters";

export const useTaskFilters = () => {
  const [searchParams, setSearchParams] = useSearchParams();

  const updateTaskFilter = (newFilters: Partial<TaskFilters>) => {
    setSearchParams((prev) => {
      const current = Object.fromEntries(prev.entries());

      const formattedNewFilters: Record<string, string> = {};

      Object.entries(newFilters).forEach(([key, value]) => {
        if (value !== undefined && value !== null && value !== "") {
          formattedNewFilters[key] = String(value);
        }
      });

      const mergedFilters = { ...current, ...formattedNewFilters };

      Object.keys(newFilters).forEach((key) => {
        const value = newFilters[key as keyof TaskFilters];
        if (value === undefined || value === null || value === "") {
          delete mergedFilters[key];
        }
      });

      return mergedFilters;
    });
  };

  const resetFilters = () => {
    searchParams.delete("todoListId");
    searchParams.delete("name");
    searchParams.delete("isCompleted");
    searchParams.delete("isOverdue");
    setSearchParams(searchParams);
  };

  const filters: TaskFilters = {
    todoListId: searchParams.get("todoListId")
      ? Number(searchParams.get("todoListId"))
      : undefined,
    name: searchParams.get("name") || undefined,
    isCompleted: searchParams.get("isCompleted") === "true" ? true : undefined,
    isOverdue: searchParams.get("isOverdue") === "true" ? true : undefined,
  };

  return { filters, resetFilters, updateTaskFilter };
};
