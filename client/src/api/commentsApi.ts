import type { CreateTaskCommentRequest } from "../dto/requests/taskComments/CreateTaskCommentRequest";
import type { TaskCommentResponse } from "../dto/responses/TaskCommentRespose";
import { api } from "./instance/axiosInstance";

export const commentsApi = {
  create: async (
    data: CreateTaskCommentRequest,
  ): Promise<TaskCommentResponse> => {
    const response = await api.post<TaskCommentResponse>(
      "/TaskComment",
      data,
    );
    return response.data;
  },

  get: async (id: number): Promise<TaskCommentResponse> => {
    const response = await api.get<TaskCommentResponse>(`/TaskComment/${id}`);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/TaskComment/${id}`);
  },

  getAll: async (): Promise<TaskCommentResponse[]> => {
    const response = await api.get<TaskCommentResponse[]>("/TaskComment");
    return response.data;
  },
};
