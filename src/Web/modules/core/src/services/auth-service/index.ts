import { apiClient } from '../../api-client';

const BASE_URL = '/user-service/api/users';

interface SignUpRequest {
  username: string;
  email: string;
  password: string;
}

const signUp = async (request: SignUpRequest) => {
  const res = await apiClient.post(`${BASE_URL}/signup`, {
    username: request.username,
    email: request.email,
    password: request.password,
  });
  const result = res.data;
  return result;
};

interface SignInRequest {
  email: string;
  password: string;
}

const signIn = async (request: SignInRequest) => {
  const res = await apiClient.post(`${BASE_URL}/signin`, { email: request.email, password: request.password });
  const result = res.data;
  return result;
};

const checkAuth = async () => {
  const res = await apiClient.get(`${BASE_URL}/check-auth`);
  const result = res.data;
  return result;
};

export const authService = Object.freeze({ signUp, signIn, checkAuth });
