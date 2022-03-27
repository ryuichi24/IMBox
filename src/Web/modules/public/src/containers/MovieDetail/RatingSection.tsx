import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { Demographic } from '@IMBoxWeb/core/dist/models';
import { ratingService } from '@IMBoxWeb/core/dist/services';
import { StarRating } from '@/components/StarRating';
import { useAuthContext } from '@/contexts/auth-context';

export const RatingSection = () => {
  const { movieId } = useParams();
  const [ratingAnalysis, setRatingAnalysis] = useState();
  const [rating, setRating] = useState(0);

  const { user, isAuthenticated } = useAuthContext();

  useEffect(() => {
    if (!movieId) return;
    (async () => {
      const ratingAnalysis = await ratingService.getRatingAnalysis({ movieId, demographic: Demographic.all });
      console.log(ratingAnalysis);
      setRatingAnalysis(ratingAnalysis);
      setRating(Math.trunc((ratingAnalysis as any)?.averageRating));
    })();
  }, []);

  return (
    <>
      <div className="d-flex justify-content-between">
        <RatingContainer>
          <Link className="text-decoration-none text-white" to={`/movies/${movieId}/ratings`}>
            <div>
              <RatingNumWrapper>{rating}</RatingNumWrapper> / <MaxRatingNumWrapper>5</MaxRatingNumWrapper>
            </div>
            <StarRating rating={rating} displayOnly={true} />
            <small>{(ratingAnalysis as any)?.totalRatingVoteCount} votes</small>
          </Link>
        </RatingContainer>
        {isAuthenticated && (
          <div>
            <button className="btn btn-warning">Rate</button>
          </div>
        )}
      </div>
    </>
  );
};

const RatingContainer = styled.div`
  width: fit-content;
  &:hover {
    background-color: #303030;
  }
`;

const MaxRatingNumWrapper = styled.span`
  font-size: 20px;
`;

const RatingNumWrapper = styled.span`
  font-size: 25px;
  font-weight: 600;
`;
