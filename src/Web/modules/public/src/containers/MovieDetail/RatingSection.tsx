import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { Demographic, RatingAnalyticsDTO } from '@IMBoxWeb/core/dist/models';
import { ratingService } from '@IMBoxWeb/core/dist/services';
import { StarRating } from '@/components/StarRating';
import { useAuthContext } from '@/contexts/auth-context';
import { RatingForm } from './RatingForm';

export const RatingSection = () => {
  const { movieId } = useParams();
  const [ratingAnalytics, setRatingAnalytics] = useState<RatingAnalyticsDTO>();
  const [showRatingForm, setShowRatingForm] = useState(false);

  const { isAuthenticated } = useAuthContext();

  useEffect(() => {
    if (!movieId) return;
    (async () => {
      const ratingAnalytics = await ratingService.getRatingAnalytics({ movieId, demographic: Demographic.all });
      setRatingAnalytics(ratingAnalytics);
    })();
  }, []);

  const handleRate = async () => {
    if (!movieId) return;

    const ratingAnalytics = await ratingService.getRatingAnalytics({ movieId, demographic: Demographic.all });
    setRatingAnalytics(ratingAnalytics);
  };

  return (
    <>
      <div className="d-flex justify-content-between">
        <RatingContainer>
          <Link className="text-decoration-none text-white" to={`/movies/${movieId}/ratings`}>
            <div>
              <RatingNumWrapper>{ratingAnalytics?.averageRating.toFixed(1)}</RatingNumWrapper> /{' '}
              <MaxRatingNumWrapper>5</MaxRatingNumWrapper>
            </div>
            <StarRating rating={Math.trunc(ratingAnalytics?.averageRating || 0)} displayOnly={true} />
            <small>{ratingAnalytics?.totalRatingVoteCount} votes</small>
          </Link>
        </RatingContainer>
        {isAuthenticated && (
          <div>
            <button className="btn btn-warning" onClick={(e) => setShowRatingForm(true)}>
              Rate
            </button>
          </div>
        )}
      </div>
      {showRatingForm && (
        <RatingForm
          handleClose={() => setShowRatingForm(false)}
          handleRate={handleRate}
          ratingAnalytics={ratingAnalytics}
        />
      )}
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
