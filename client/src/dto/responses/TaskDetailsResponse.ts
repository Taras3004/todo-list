import type { TaskTagResponse } from "./TaskTagResponse";

export interface TaskDetailsResponse {
  id: number;
  name: string;
  deadline: Date;
  isCompleted: boolean;
  description?: string;
  todoListId: number;
  taskTags: TaskTagResponse[];
}
