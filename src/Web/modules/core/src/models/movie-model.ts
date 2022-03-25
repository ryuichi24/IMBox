export interface MovieModel {
  id?: string;
  title?: string;
  description?: string;
  commentCount?: number;
  mainPosterUrl?: string;
  mainTrailerUrl?: string;
  otherPostUrls?: string[];
  otherTrailerUrls?: string[];
  memberIds?: string[];
  members?: any[];
}