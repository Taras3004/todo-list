import { Outlet, Link } from "react-router-dom";
import { useState } from "react";
import { Button } from "../Components/Button";
import { Menu } from "lucide-react";
import { LogOut } from "lucide-react";

export const MainLayout = () => {
  const [isMenuOpen, ToggleMenu] = useState(true);
  const lists = ["list 1", "list 2", "list 3", "list 4", "list 1"];

  return (
    <div className="flex-col">
      <nav className="flex items-center p-4 gap-5 bg-gray-200">
        <button onClick={() => ToggleMenu(!isMenuOpen)}>
          <Menu />
        </button>
        <Link to="/tasks">Tasks</Link>
        <Link to="/taskTags">Tags</Link>
        <Link to="/login" className="ml-auto">
          <LogOut />
        </Link>
      </nav>

      <div className="flex flex-1 overflow-hidden">
        <div
          className={`${
            isMenuOpen ? "w-2xs" : "w-0"
          } bg-gray-400 rounded-2xl overflow-hidden whitespace-nowrap
          transition-all duration-300 ease-in-out`}
        >
          <div className="h-full p-6">
            <h1 className="text-center font-bold text-2xl mb-2">Lists</h1>
            <ul>
              {lists.map((list) => (
                <li className="">
                  <Button onClick={() => console.log("clicked on list")}>
                    {list}
                  </Button>
                </li>
              ))}
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
