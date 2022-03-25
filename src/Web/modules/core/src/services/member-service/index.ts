import { apiClient } from '../../api-client';
import { MemberModel } from '../../models/member-model';

const BASE_URL = '/member-service/api';

interface GetMembersRequest {
  page: number | string;
}

const get = async (request: GetMembersRequest) => {
  const res = await apiClient.get(`${BASE_URL}/members`);
  const result = res.data;
  return result as MemberModel[];
};

interface GetMemberByIdRequest {
  memberId: string;
}

const getById = async (request: GetMemberByIdRequest) => {
  const res = await apiClient.get(`${BASE_URL}/members/${request.memberId}`);
  const result = res.data;
  return result as MemberModel;
};

interface CreateMemberRequest {
  member: MemberModel;
}

const create = async (request: CreateMemberRequest) => {
  const res = await apiClient.post(`${BASE_URL}/members`, request.member);
  const result = res.data;
  return result;
};

interface UpdateMemberRequest {
  memberId: string;
  member: MemberModel;
}

const update = async (request: UpdateMemberRequest) => {
  const res = await apiClient.patch(`${BASE_URL}/members/${request.memberId}`, request.member);
  const result = res.data;
  return result;
};

interface RemoveMemberRequest {
  memberId: number | string;
}

const remove = async (request: RemoveMemberRequest) => {
  const res = await apiClient.delete(`${BASE_URL}/members/${request.memberId}`);
  const result = res.data;
  return result;
};

export const memberService = Object.freeze({ get, getById, create, update, remove });
