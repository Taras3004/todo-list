import { Square, SquareCheck } from "lucide-react";

interface CheckBoxProps {
  isActive: boolean;
  onClick?: (e: React.MouseEvent) => void;
}

export const CheckBox = ({ isActive, onClick }: CheckBoxProps) => {
  return (
    <div className="text-foreground" onClick={onClick}>
      {isActive ? <SquareCheck /> : <Square />}
    </div>
  );
};
