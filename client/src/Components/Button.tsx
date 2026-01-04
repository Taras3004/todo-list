import { type ReactNode } from "react";

interface ButtonProps {
  onClick: () => void;
  children?: ReactNode;
}

export const Button = ({ onClick, children }: ButtonProps) => {
  return (
    <button
      type="button"
      onClick={onClick}
      className="w-full text-left bg-blue-200 rounded-2xl shadow mb-2 p-2
                 hover:bg-blue-300 active:bg-blue-400 transition-colors duration-200"
    >
      {children}
    </button>
  );
};
