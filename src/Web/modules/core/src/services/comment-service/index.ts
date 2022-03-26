import { apiClient } from '../../api-client';
import { CommentModel } from '../../models/comment-model';

const BASE_URL = '/comment-service/api';

interface GetCommentsRequest {
  page: number | string;
  movieId: string;
}

const get = async (request: GetCommentsRequest) => {
  const res = await apiClient.get(`${BASE_URL}/comments?movieId=${request.movieId}`);
  const result = res.data;
  return result as CommentModel[];
};

interface GetCommentByIdRequest {
  commentId: string;
}

const getById = async (request: GetCommentByIdRequest) => {
  const res = await apiClient.get(`${BASE_URL}/comments/${request.commentId}`);
  const result = res.data;
  return result as CommentModel;
};

interface CreateCommentRequest {
  member: CommentModel;
}

const create = async (request: CreateCommentRequest) => {
  const res = await apiClient.post(`${BASE_URL}/comments`, request.member);
  const result = res.data;
  return result;
};

interface UpdateMemberRequest {
  commentId: string;
  comment: CommentModel;
}

const update = async (request: UpdateMemberRequest) => {
  const res = await apiClient.patch(`${BASE_URL}/comments/${request.commentId}`, request.comment);
  const result = res.data;
  return result;
};

interface RemoveCommentRequest {
  commentId: number | string;
}

const remove = async (request: RemoveCommentRequest) => {
  const res = await apiClient.delete(`${BASE_URL}/comments/${request.commentId}`);
  const result = res.data;
  return result;
};

export const commentService = Object.freeze({ get, getById, create, update, remove });
