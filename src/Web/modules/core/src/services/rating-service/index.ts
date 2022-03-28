import { apiClient } from '../../api-client';
import { Demographic, RatingModel } from '../../models';

const BASE_URL = '/rating-service/api';

interface GetRatingAnalyticsRequest {
  movieId: string;
  demographic: Demographic;
}

const getRatingAnalytics = async (request: GetRatingAnalyticsRequest) => {
  const res = await apiClient.get(`${BASE_URL}/movies/${request.movieId}/ratings?demographic=${request.demographic}`);
  const result = res.data;
  return result;
};

interface GetUserRatingsRequest {
  raterId: string;
}

const getUsesRatings = async (request: GetUserRatingsRequest) => {
  const res = await apiClient.get(`${BASE_URL}/raters/${request.raterId}/ratings`);
  const result = res.data;
  return result;
};

interface CreateRatingRequest {
  rating: RatingModel;
}

const create = async (request: CreateRatingRequest) => {
  const res = await apiClient.post(`${BASE_URL}/ratings`, request.rating);
  const result = res.data;
  return result;
};

interface UpdateRatingRequest {
  ratingId: string;
  rating: RatingModel;
}

const update = async (request: UpdateRatingRequest) => {
  const res = await apiClient.patch(`${BASE_URL}/ratings/${request.ratingId}`, request.rating);
  const result = res.data;
  return result;
};

interface RemoveRatingRequest {
  ratingId: string;
}

const remove = async (request: RemoveRatingRequest) => {
  const res = await apiClient.delete(`${BASE_URL}/ratings/${request.ratingId}`);
  const result = res.data;
  return result;
};

export const ratingService = Object.freeze({ create, update, remove, getRatingAnalytics, getUsesRatings });
