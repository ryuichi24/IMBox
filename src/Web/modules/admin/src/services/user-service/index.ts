import { UserModel } from '@/models/user-model';
import { apiClient } from '@IMBoxWeb/core/dist/api-client';

const BASE_URL = '/user-service/api';

interface GetUsersRequest {
  page: number | string;
}

const get = async (request: GetUsersRequest) => {
  const res = await apiClient.get(`${BASE_URL}/users`);
  const result = res.data;
  return result as UserModel[];
};

interface GetUserByIdRequest {
  userId: string;
}

const getById = async (request: GetUserByIdRequest) => {
  const res = await apiClient.get(`${BASE_URL}/users/${request.userId}`);
  const result = res.data;
  return result as UserModel;
};

interface CreateUserRequest {
  user: UserModel;
}

const create = async (request: CreateUserRequest) => {
  const res = await apiClient.post(`${BASE_URL}/users`, request.user);
  const result = res.data;
  return result;
};

interface UpdateUserRequest {
  userId: string;
  user: UserModel;
}

const update = async (request: UpdateUserRequest) => {
  const res = await apiClient.patch(`${BASE_URL}/users/${request.userId}`, request.user);
  const result = res.data;
  return result;
};

interface RemoveUserRequest {
  userId: number | string;
}

const remove = async (request: RemoveUserRequest) => {
  const res = await apiClient.delete(`${BASE_URL}/users/${request.userId}`);
  const result = res.data;
  return result;
};

export const userService = Object.freeze({ get, getById, create, update, remove });
