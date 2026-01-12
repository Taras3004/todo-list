import { Transition } from "@headlessui/react";
import { useState } from "react";
import { Button } from "./Button";
import { TagContainer } from "./TagContainer";

export const TagPopup = () => {
  const initialTags = [
    { id: 1, name: "tag 1" },
    { id: 2, name: "tag 2" },
    { id: 3, name: "tag 3" },
    { id: 4, name: "tag 4" },
    { id: 5, name: "tag 1" },
  ];
  const [tags, setTags] = useState(initialTags);
  const [createdTag, setCreatedTag] = useState("");
  const [isTagHovered, setTagIsHovered] = useState(false);

  let newID: number = 6;
  const addTag = (name: string) => {
    setTags([...tags, { id: newID, name: name }]);
    newID++;
  };

  const deleteTag = (id: number) => {
    setTags(tags.filter((tag) => tag.id != id));
  };

  const HandleTagCreation = (e: React.FormEvent) => {
    e.preventDefault();
    if (createdTag.trim() === "") return;
    addTag(createdTag);
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
                  tag={tag.name}
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
