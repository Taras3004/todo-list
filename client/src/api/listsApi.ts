import type { CreateListRequest } from "../dto/requests/lists/CreateListRequest";
import type { UpdateListRequest } from "../dto/requests/lists/UpdateListRequest";
import type { TodoListResponse } from "../dto/responses/TodoListResponse";
import { api } from "./axiosInstance";

export const listsApi = {
  create: async (data: CreateListRequest): Promise<TodoListResponse> => {
    const response = await api.post<TodoListResponse>("/TodoList", data);
    return response.data;
  },

  get: async (id: number): Promise<TodoListResponse> => {
    const response = await api.get<TodoListResponse>(`/TodoList/${id}`);
    return response.data;
  },

  update: async (data: UpdateListRequest): Promise<TodoListResponse> => {
    const response = await api.put<TodoListResponse>("/TodoList", data);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/TodoList/${id}`);
  },

  getAll: async (): Promise<TodoListResponse[]> => {
    const response = await api.get<TodoListResponse[]>("/TodoList");
    return response.data;
  },
};
