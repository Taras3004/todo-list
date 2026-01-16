import { Button } from "./Button";
import { Dot, X } from "lucide-react";

interface TagContainerProps {
  tag: string;
  onClick: () => void;
}

export const TagContainer = ({ tag, onClick }: TagContainerProps) => (
  <Button className="mb-2" onClick={onClick}>
    <div className="flex items-center justify-center">
      <Dot />
      <p>{tag}</p>
      <X className="ml-1 mr-1" size={15} />
    </div>
  </Button>
);
