import { apiClient } from '@IMBoxWeb/core/dist/api-client';

const BASE_URL = '/user-service/api/users';

interface SignInRequest {
  email: string;
  password: string;
}

const signIn = async (request: SignInRequest) => {
  const res = await apiClient.post(`${BASE_URL}/signin`, { email: request.email, password: request.password });
  const result = res.data;
  return result;
};

export const authService = Object.freeze({ signIn });
