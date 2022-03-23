import { MovieModel } from '@/models/movie-model';
import { apiClient } from '@IMBoxWeb/core/dist/api-client';

const BASE_URL = '/movie-service/api';

interface GetMoviesRequest {
  page: number | string;
}

const get = async (request: GetMoviesRequest) => {
  const res = await apiClient.get(`${BASE_URL}/movies`);
  const result = res.data;
  return result as MovieModel[];
};

interface GetMovieByIdRequest {
  movieId: string;
}

const getById = async (request: GetMovieByIdRequest) => {
  const res = await apiClient.get(`${BASE_URL}/movies/${request.movieId}`);
  const result = res.data;
  return result as MovieModel;
};

interface CreateMovieRequest {
  movie: MovieModel;
}

const create = async (request: CreateMovieRequest) => {
  const res = await apiClient.post(`${BASE_URL}/movies`, request.movie);
  const result = res.data;
  return result;
};

interface UpdateMovieRequest {
  movieId: string;
  movie: MovieModel;
}

const update = async (request: UpdateMovieRequest) => {
  const res = await apiClient.patch(`${BASE_URL}/movies/${request.movieId}`, request.movie);
  const result = res.data;
  return result;
};

interface RemoveMovieRequest {
  movieId: number | string;
}

const remove = async (request: RemoveMovieRequest) => {
  const res = await apiClient.delete(`${BASE_URL}/movies/${request.movieId}`);
  const result = res.data;
  return result;
};

export const movieService = Object.freeze({ get, getById, create, update, remove });
