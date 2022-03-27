export interface RatingModel {
  movieId: string;
  rating: Rating;
}

export enum Rating {
  one = 1,
  two = 2,
  three = 3,
  four = 4,
  five = 5,
}

export enum Demographic {
  all = 'all',
  males = 'males',
  females = 'females',

  agedUnder18 = 'females_aged_under_18',
  aged18To29 = 'females_aged_18_29',
  aged30To44 = 'females_aged_30_40',
  aged45Plus = 'females_aged_45_plus',

  malesAgedUnder18 = 'females_aged_under_18',
  malesAged18To29 = 'females_aged_18_29',
  malesAged30To44 = 'females_aged_30_40',
  malesAged45Plus = 'females_aged_45_plus',

  femalesAgedUnder18 = 'females_aged_under_18',
  femalesAged18To29 = 'females_aged_18_29',
  femalesAged30To44 = 'females_aged_30_40',
  femalesAged45Plus = 'females_aged_45_plus',
}
