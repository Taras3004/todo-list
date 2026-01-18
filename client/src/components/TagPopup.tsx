import { useState } from "react";
import { Button } from "./Button";
import { TagContainer } from "./TagContainer";
import { CircleLoader } from "./CircleLoader";
import { Popup } from "./Popup";
import { useTagsContext } from "../context/TagsContext";

export const TagPopup = () => {
  const { tags, isLoading, error, createTag, deleteTag } = useTagsContext();
  const [createdTag, setCreatedTag] = useState("");

  const handleTagCreation = (e: React.FormEvent) => {
    e.preventDefault();
    if (createdTag.trim() === "") return;
    createTag({ tag: createdTag });
    setCreatedTag("");
  };

  return (
    <Popup trigger={<div>Tags</div>}>
      {isLoading ? (
        <CircleLoader />
      ) : (
        <div className="flex flex-col overflow-y-auto max-h-50 pr-3.5">
          <Button className="mb-2">
            <form onSubmit={handleTagCreation}>
              <input
                type="text"
                placeholder="create"
                value={createdTag}
                onChange={(e) => setCreatedTag(e.target.value)}
                className="w-20 outline-none text-center bg-transparent"
              />
            </form>
          </Button>

          {tags.map((tag) => (
            <TagContainer
              key={tag.id}
              tag={tag.tag}
              onClick={() => deleteTag(tag.id)}
            />
          ))}
        </div>
      )}
    </Popup>
  );
};
