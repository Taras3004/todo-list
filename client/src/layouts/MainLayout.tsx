import { Outlet, Link } from "react-router-dom";
import { useState } from "react";
import { Menu, LogOut } from "lucide-react";
import { Button } from "../components/Button";
import { TagPopup } from "../components/TagPopup";
import { ThemeSwitcher } from "../components/ThemeSwitcher";
import { SearchBar } from "../components/SearchBar";

export const MainLayout = () => {
  const [isMenuOpen, ToggleMenu] = useState(true);

  const initialLists = [
    { id: 1, name: "list 1" },
    { id: 2, name: "list 2" },
    { id: 3, name: "list 3" },
    { id: 4, name: "list 4" },
    { id: 5, name: "list 1" },
  ];

  const [lists, setLists] = useState(initialLists);
  const [newList, setNewList] = useState("");

  const newListID: number = 6; // temp
  const HandleListCreation = (e: React.FormEvent) => {
    e.preventDefault();

    if (newList.trim() === "") return;
    setLists([...lists, { id: newListID, name: newList }]);
    setNewList("");
  };

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
            <ul>
              {lists.map((list) => (
                <li>
                  <Button
                    onClick={() => console.log("clicked on list")}
                    className="text-center w-full mb-2"
                  >
                    {list.name}
                  </Button>
                </li>
              ))}
              <li>
                <Button>
                  <form onSubmit={HandleListCreation}>
                    <input
                      type="text"
                      placeholder="new list..."
                      value={newList}
                      onChange={(e) => setNewList(e.target.value)}
                      className="w-full outline-none text-center"
                    ></input>
                  </form>
                </Button>
              </li>
            </ul>
          </div>
        </div>
        <main className="p-6 h-screen text-center w-full">
          <Outlet />
        </main>
      </div>
    </div>
  );
};
