import type { LoginRequest } from "../dto/requests/auth/LoginRequest";
import type { RegisterRequest } from "../dto/requests/auth/RegisterRequest";
import type { LoginResponse } from "../dto/responses/LoginResponse";
import { api } from "./instance/axiosInstance";

export const authApi = {
  login: async (data: LoginRequest): Promise<LoginResponse> => {
    var response = await api.post<LoginResponse>("/auth/login", data);
    return response.data;
  },

  register: async (data: RegisterRequest): Promise<void> => {
    await api.post("/auth/register", data);
  },
};
