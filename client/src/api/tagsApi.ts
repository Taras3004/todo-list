import type { CreateTaskTagRequest } from "../dto/requests/taskTags/CreateTaskTagRequest";
import type { UpdateTaskTagRequest } from "../dto/requests/taskTags/UpdateTaskTagRequest";
import type { TaskTagResponse } from "../dto/responses/TaskTagResponse";
import { api } from "./instance/axiosInstance";

export const tagsApi = {
  create: async (data: CreateTaskTagRequest): Promise<TaskTagResponse> => {
    const response = await api.post<TaskTagResponse>("/TaskTag", data);
    return response.data;
  },

  get: async (id: number): Promise<TaskTagResponse> => {
    const response = await api.get<TaskTagResponse>(`/TaskTag/${id}`);
    return response.data;
  },

  update: async (data: UpdateTaskTagRequest): Promise<TaskTagResponse> => {
    const response = await api.put<TaskTagResponse>("/TaskTag", data);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/TaskTag/${id}`);
  },

  getAll: async (): Promise<TaskTagResponse[]> => {
    const response = await api.get<TaskTagResponse[]>("/TaskTag");
    return response.data;
  },
};
