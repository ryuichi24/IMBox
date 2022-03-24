import axios, { AxiosError } from 'axios';
import { tokenManager } from './token-manager';

const refreshTokenEndpoint = process.env.REFRESH_TOKEN_URL || '/user-service/api/users/refresh-token';

export const apiClient = axios.create({
  baseURL: process.env.API_URL || 'http://localhost:5555',
});

const retryAxios = axios.create({ baseURL: process.env.API_URL || 'http://localhost:5555' });

retryAxios.interceptors.response.use(
  (response) => {
    return response;
  },
  async (err: AxiosError) => {
    const { config, response } = err;

    if (config.url === refreshTokenEndpoint) {
      tokenManager.refreshToken.remove();
      throw err;
    }
  },
);

apiClient.interceptors.response.use(
  (response) => {
    const result = response.data;

    if (result?.accessToken?.token) {
      tokenManager.accessToken.set(result.accessToken.token, result.accessToken.expiresIn);
    }

    if (result?.refreshToken?.token) {
      tokenManager.refreshToken.set(result.refreshToken.token);
    }

    return response;
  },
  async (err: AxiosError) => {
    const { config, response } = err;

    if (response?.status !== 401) throw err;

    const refreshToken = tokenManager.refreshToken.get();

    if (!refreshToken) throw err;

    const res = await retryAxios.post(refreshTokenEndpoint, { refreshToken });

    const result = res?.data;

    if (result?.accessToken?.token) {
      tokenManager.accessToken.set(result.accessToken.token, result.accessToken.expiresIn);
    }

    if (result?.refreshToken?.token) {
      tokenManager.refreshToken.set(result.refreshToken.token);
    }

    if (config.headers) config.headers.Authorization = `Bearer ${result?.accessToken?.token}`;

    return await apiClient.request(config);
  },
);

apiClient.interceptors.request.use((request) => {
  const accessToken = tokenManager.accessToken.get();
  if (request.headers && accessToken) request.headers.Authorization = `Bearer ${accessToken}`;

  return request;
});
