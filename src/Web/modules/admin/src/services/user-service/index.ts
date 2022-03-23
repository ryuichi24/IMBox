import { apiClient } from '@IMBoxWeb/core/dist/api-client';

const BASE_URL = '/user-service/api/users';

interface GetUsersRequest {
  page: number | string;
}

const get = async (request: GetUsersRequest) => {
  const res = await apiClient.get(`${BASE_URL}`);
  const result = res.data;
  return result;
};

export const userService = Object.freeze({ get });
