export interface TaskResponse {
  id: number;
  name: string;
  deadline: Date;
  isCompleted: boolean;
  todoListId: number;
}
