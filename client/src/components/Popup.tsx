import { Transition } from "@headlessui/react";
import { useState, type ReactNode } from "react";

interface PopupProps {
  trigger: ReactNode;
  children: ReactNode;
  className?: string;
}

export const Popup = ({ trigger, children, className = "" }: PopupProps) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div
      className="relative inline-block"
      onMouseEnter={() => setIsOpen(true)}
      onMouseLeave={() => setIsOpen(false)}
    >
      <div className="cursor-pointer">{trigger}</div>

      <Transition
        show={isOpen}
        enter="transition ease-out duration-200"
        enterFrom="opacity-0 translate-y-1"
        enterTo="opacity-100 translate-y-0"
        leave="transition ease-in duration-150"
        leaveFrom="opacity-100 translate-y-0"
        leaveTo="opacity-0 translate-y-1"
      >
        <div className={`absolute z-10 ${className}`}>
          <div className="bg-secondary rounded-b-2xl rounded-tr-2xl p-2 shadow-2xl">
            {children}
          </div>
        </div>
      </Transition>
    </div>
  );
};
