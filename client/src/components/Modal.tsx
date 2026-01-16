import { useNavigate, useSearchParams } from "react-router-dom";
import type React from "react";

interface ModalProps {
  children: React.ReactNode;
  title?: string;
  onModalClosed?: () => void;
}

export const Modal = ({ children, title, onModalClosed }: ModalProps) => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const closeModal = () => {
    navigate(`..?${searchParams.toString()}`, { relative: "path" });
  };

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-sm p-4"
      onClick={() => {
        onModalClosed?.();
        closeModal();
      }}
    >
      <div
        className="bg-background rounded-default shadow-2xl w-full max-w-md overflow-hidden relative animate-in zoom-in-95 duration-200"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="flex justify-between items-center p-4">
          <h2 className="text-xl font-bold text-foreground">
            {title || "Details"}
          </h2>
        </div>

        <div className="p-0">{children}</div>
      </div>
    </div>
  );
};
