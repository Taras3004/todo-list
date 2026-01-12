import { Switch } from "@headlessui/react";
import { Moon, Sun } from "lucide-react";
import { useState } from "react";

interface ThemeSwitcherProps {
  className?: string;
}

export const ThemeSwitcher = ({ className }: ThemeSwitcherProps) => {
  const [isLightTheme, setIsLightTheme] = useState(true);

  const root = window.document.documentElement;
  const ToggleTheme = () => {
    setIsLightTheme(!isLightTheme);
    console.log("set");
    if (isLightTheme) {
      root.classList.add("dark");
    } else {
      root.classList.remove("dark");
    }
  };

  return (
    <Switch
      checked={isLightTheme}
      onChange={ToggleTheme}
      className={`group flex h-7 w-15 items-center rounded-full bg-background transition  ${className}`}
    >
      <div className="size-6 translate-x-1 transition group-data-checked:translate-x-8 items-center">
        {isLightTheme ? <Sun /> : <Moon />}
      </div>
    </Switch>
  );
};
