import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';
import { Demographic, MovieModel } from '@IMBoxWeb/core/dist/models';
import { StarRating } from '../StarRating';
import { ratingService } from '@IMBoxWeb/core/dist/services';

interface Props {
  movieItem: MovieModel;
}

export const MovieItem = ({ movieItem }: Props) => {
  const [averageRating, setAverageRating] = useState<number>(0);
  useEffect(() => {
    (async () => {
      if (!movieItem.id) return;
      const ratingAnalytics = await ratingService.getRatingAnalytics({
        movieId: movieItem.id,
        demographic: Demographic.all,
      });
      setAverageRating(ratingAnalytics.averageRating);
    })();
  }, []);

  return (
    <div
      className="card p-2 shadow-sm w-25"
      style={{ backgroundColor: '#1a1a1a', minHeight: '400px' }}
      key={movieItem.id}
    >
      <h3>{movieItem.title}</h3>
      <Link to={`/movies/${movieItem.id}`}>
        <img src={movieItem.mainPosterUrl} alt={movieItem.title} className="w-100" />
      </Link>
      <div className="card-body">
        <p className="text-truncate">{movieItem.description}</p>
        <p style={{ color: '#929292' }} className="text-truncate">
          <span className="fw-bold text-white">{movieItem.commentCount}</span> comments
        </p>
        <div>
          <RatingNumWrapper>{averageRating.toFixed(1)}</RatingNumWrapper> / <MaxRatingNumWrapper>5</MaxRatingNumWrapper>
        </div>
        <StarRating rating={Math.trunc(averageRating)} displayOnly={true} />
      </div>
    </div>
  );
};

const MaxRatingNumWrapper = styled.span`
  font-size: 20px;
`;

const RatingNumWrapper = styled.span`
  font-size: 25px;
  font-weight: 600;
`;
