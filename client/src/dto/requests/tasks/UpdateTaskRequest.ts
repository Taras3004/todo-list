export interface UpdateTaskRequest {
  id: number;
  name: string;
  deadline: Date;
  isCompleted: boolean;
  description?: string;
}
