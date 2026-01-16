import { Search } from "lucide-react";
import { useEffect, useState } from "react";
import { useTaskFilters } from "../hooks/useTaskFilters";

export const SearchBar = () => {
  const { filters, updateTaskFilter } = useTaskFilters();

  const initialNameParam = filters["name"] || "";
  const [nameParam, setNameParam] = useState(initialNameParam);

  useEffect(() => {
    setNameParam(filters["name"] || "");
  }, [filters["name"]]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (nameParam.trim().length === 0) {
      updateTaskFilter({ name: undefined });
    } else {
      updateTaskFilter({ name: nameParam });
    }
  };

  return (
    <div className="flex gap-1 bg-background rounded-default py-1 px-2">
      <Search />
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="search"
          value={nameParam}
          onChange={(e) => setNameParam(e.target.value)}
          className="outline-none w-full"
        />
      </form>
    </div>
  );
};
