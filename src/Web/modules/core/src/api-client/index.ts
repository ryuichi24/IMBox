import axios, { AxiosError } from 'axios';
import { tokenManager } from './token-manager';

const refreshTokenEndpoint =
  process.env.REFRESH_TOKEN_URL || 'http://apigateway:80/user-service/api/users/refresh-token';

export const apiClient = axios.create({
  baseURL: process.env.API_URL,
  withCredentials: true,
});

const retryAxios = axios.create({ baseURL: process.env.API_URL, withCredentials: true });

apiClient.interceptors.response.use(
  (response) => {
    const result = response.data;

    if (result?.accessToken?.value) {
      tokenManager.accessToken.set(result.accessToken.value, result.accessToken.expiresIn);
    }

    if (result?.refreshToken?.value) {
      tokenManager.refreshToken.set(result.refreshToken.value);
    }

    return response;
  },
  async (err: AxiosError) => {
    const { config, response } = err;

    if (response?.status !== 401) throw err;

    if (config.url === refreshTokenEndpoint) {
      tokenManager.refreshToken.remove();
      throw err;
    }

    const refreshToken = tokenManager.refreshToken.get();
    if (!refreshToken) throw err;

    const res = await retryAxios.post(refreshTokenEndpoint, { refreshToken });
    const result = res.data;

    if (result?.accessToken?.value) {
      tokenManager.accessToken.set(result.accessToken.value, result.accessToken.expiresIn);
    }

    if (result?.refreshToken?.value) {
      tokenManager.refreshToken.set(result.refreshToken.value);
    }

    return await apiClient(config);
  },
);

apiClient.interceptors.request.use((request) => {
  const accessToken = tokenManager.accessToken.get();
  if (request.headers && accessToken) request.headers.Authorization = `Bearer ${accessToken}`;

  return request;
});
