import { type ReactNode } from "react";

interface ButtonProps {
  onClick?: () => void;
  children?: ReactNode;
  className?: string;
}

export const Button = ({ onClick, children, className }: ButtonProps) => {
  return (
    <button
      type="button"
      onClick={onClick}
      className={`bg-card text-card-foreground rounded-default p-2
                 hover:bg-card-highlighted active:bg-card-clicked transition-colors duration-200 ${className}`}
    >
      {children}
    </button>
  );
};
