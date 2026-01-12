import { Search } from "lucide-react";
import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

export const SearchBar = () => {
  const [searchParams, setSearchParams] = useSearchParams();

  const initialNameParam = searchParams.get("name") || "";
  const [nameParam, setNameParam] = useState(initialNameParam);

  useEffect(() => {
    setNameParam(searchParams.get("name") || "");
  }, [searchParams]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (nameParam.trim().length === 0) {
      setSearchParams({});
    } else {
      setSearchParams({ name: nameParam });
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
