import type { CreateTaskRequest } from "../dto/requests/tasks/CreateTaskRequest";
import type { TaskResponse } from "../dto/responses/TaskResponse";
import type { UpdateTaskRequest } from "../dto/requests/tasks/UpdateTaskRequest";
import type { TaskDetailsResponse } from "../dto/responses/TaskDetailsResponse";
import type { AddTaskTagRequest } from "../dto/requests/tasks/AddTaskTagRequest";
import { api } from "./axiosInstance";
import type { TaskTagResponse } from "../dto/responses/TaskTagResponse";

export const tasksApi = {
  create: async (data: CreateTaskRequest): Promise<TaskResponse> => {
    const response = await api.post<TaskResponse>("/task", data);
    return response.data;
  },

  get: async (id: number): Promise<TaskDetailsResponse> => {
    const response = await api.get<TaskDetailsResponse>(`/task/${id}`);
    return response.data;
  },

  update: async (data: UpdateTaskRequest): Promise<TaskDetailsResponse> => {
    const response = await api.put<TaskDetailsResponse>("/task", data);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete<TaskResponse>(`/task/${id}`);
  },

  getAll: async (todoListId: number): Promise<TaskResponse[]> => {
    const response = await api.get<TaskResponse[]>(
      `/task?todoListId=${todoListId}`,
    );
    return response.data;
  },

  addTag: async (taskId: number, request: AddTaskTagRequest): Promise<void> => {
    await api.post(`/task/${taskId}/tags`, request);
  },

  removeTag: async (taskId: number, tagId: number): Promise<void> => {
    await api.delete(`/task/${taskId}/tags/${tagId}`);
  },

  getTags: async (taskId: number): Promise<TaskTagResponse[]> => {
    const response = await api.get<TaskTagResponse[]>(`/task/${taskId}/tags`);
    return response.data;
  },
};
