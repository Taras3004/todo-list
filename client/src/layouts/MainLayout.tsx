import { Outlet, Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import { Menu, LogOut, Trash2 } from "lucide-react";
import { Button } from "../components/Button";
import { TagPopup } from "../components/TagPopup";
import { ThemeSwitcher } from "../components/ThemeSwitcher";
import { SearchBar } from "../components/SearchBar";
import { useTaskFilters } from "../hooks/useTaskFilters";
import { CircleLoader } from "../components/CircleLoader";
import { useListsContext } from "../context/ListsContext";

export const MainLayout = () => {
  const [isMenuOpen, ToggleMenu] = useState(true);
  const [newList, setNewList] = useState("");
  const { lists, isLoading, createList, deleteList } = useListsContext();
  const { filters, resetFilters, updateTaskFilter } = useTaskFilters();
  const navigate = useNavigate();

  return (
    <div className="flex-col">
      <nav className="flex items-center p-4 gap-5 bg-primary">
        <button onClick={() => ToggleMenu(!isMenuOpen)}>
          <Menu
            className={`${
              isMenuOpen ? "rotate-180" : "rotate-90"
            } transition-all duration-300 ease-in-out`}
          />
        </button>
        <TagPopup />
        <SearchBar />
        <ThemeSwitcher className="ml-auto" />
        <Link to="/login">
          <LogOut />
        </Link>
      </nav>

      <div className="flex flex-1 overflow-hidden">
        <div
          className={`${
            isMenuOpen ? "w-2xs" : "w-0"
          } bg-secondary rounded-br-2xl rounded-tr-2xl overflow-hidden whitespace-nowrap
          transition-all duration-300 ease-in-out`}
        >
          <div className="h-full p-6">
            <h1 className="text-center font-bold text-2xl mb-2">Lists</h1>
            {isLoading ? (
              <CircleLoader />
            ) : (
              <ul>
                {lists.map((list) => (
                  <li>
                    <Button
                      onClick={() => {
                        resetFilters();
                        updateTaskFilter({ todoListId: list.id });
                      }}
                      className="text-center w-full mb-2 flex justify-between"
                    >
                      <p className="truncate">{list.name}</p>
                      <div
                        onClick={(e) => {
                          e.stopPropagation();
                          deleteList(list.id);
                          if (filters["todoListId"] === list.id) {
                            updateTaskFilter({ todoListId: undefined });
                            navigate("/tasks");
                          }
                        }}
                      >
                        <Trash2 />
                      </div>
                    </Button>
                  </li>
                ))}
                <li>
                  <Button>
                    <form
                      onSubmit={(e) => {
                        e.preventDefault();
                        createList({ name: newList });
                        setNewList("");
                      }}
                    >
                      <input
                        type="text"
                        placeholder="new list..."
                        value={newList}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                          setNewList(e.target.value);
                        }}
                        className="w-full outline-none text-center"
                      ></input>
                    </form>
                  </Button>
                </li>
              </ul>
            )}
          </div>
        </div>
        <main className="p-6 h-screen text-center w-full">
          <Outlet />
        </main>
      </div>
    </div>
  );
};
