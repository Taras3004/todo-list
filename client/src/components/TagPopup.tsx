import { Transition } from "@headlessui/react";
import { useState } from "react";
import { Button } from "./Button";
import { TagContainer } from "./TagContainer";
import { useTags } from "../hooks/useTags";

export const TagPopup = () => {
  const { tags, isLoading, error, createTag, deleteTag } = useTags();
  const [createdTag, setCreatedTag] = useState("");
  const [isTagHovered, setTagIsHovered] = useState(false);

  const HandleTagCreation = (e: React.FormEvent) => {
    e.preventDefault();
    if (createdTag.trim() === "") return;
    createTag({ tag: createdTag });
    setCreatedTag("");
  };

  return (
    <div
      onMouseEnter={() => setTagIsHovered(true)}
      onMouseLeave={() => setTagIsHovered(false)}
    >
      <div>Tags</div>
      <Transition
        show={isTagHovered}
        enter="transition ease-out duration-200"
        enterFrom="opacity-0 translate-y-1"
        enterTo="opacity-100 translate-y-0"
        leave="transition ease-in duration-150"
        leaveFrom="opacity-100 translate-y-0"
        leaveTo="opacity-0 translate-y-1"
      >
        <div className="absolute">
          <div className="bg-background rounded-b-2xl rounded-tr-2xl p-2 shadow-2xl">
            <div className="flex flex-col overflow-y-auto max-h-50 pr-3.5">
              <Button className="mb-2">
                <form onSubmit={HandleTagCreation}>
                  <input
                    type="text"
                    placeholder="create"
                    value={createdTag}
                    onChange={(e) => setCreatedTag(e.target.value)}
                    className="w-20 outline-none text-center"
                  ></input>
                </form>
              </Button>
              {tags.map((tag) => (
                <TagContainer
                  id={tag.id}
                  tag={tag.tag}
                  onClick={() => deleteTag(tag.id)}
                />
              ))}
            </div>
          </div>
        </div>
      </Transition>
    </div>
  );
};
