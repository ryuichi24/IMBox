export interface RatingModel {
  movieId: string;
  rating: number;
}

interface MovieDTO {
  id: string;
  title: string;
  description: string;
}

interface RatingItem {
  rating: number;
  percent: string;
  ratingVoteCount: string;
}

export interface RatingAnalyticsDTO {
  movie: MovieDTO;
  ratings: RatingItem[];
  averageRating: number;
  totalRatingVoteCount: number;
  demographicType: Demographic;
}

export enum Demographic {
  all = 'all',
  males = 'males',
  females = 'females',

  agedUnder18 = 'aged_under_18',
  aged18To29 = 'aged_18_29',
  aged30To44 = 'aged_30_44',
  aged45Plus = 'aged_45_plus',

  malesAgedUnder18 = 'males_aged_under_18',
  malesAged18To29 = 'males_aged_18_29',
  malesAged30To44 = 'males_aged_30_44',
  malesAged45Plus = 'males_aged_45_plus',

  femalesAgedUnder18 = 'females_aged_under_18',
  femalesAged18To29 = 'females_aged_18_29',
  femalesAged30To44 = 'females_aged_30_44',
  femalesAged45Plus = 'females_aged_45_plus',
}
