import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { Demographic } from '@IMBoxWeb/core/dist/models';
import { ratingService } from '@IMBoxWeb/core/dist/services';
import { StarRating } from '@/components/StarRating';
import { useAuthContext } from '@/contexts/auth-context';
import { ReusableModal } from '@/components/ReusableModal';

export const RatingSection = () => {
  const { movieId } = useParams();
  const [ratingAnalysis, setRatingAnalysis] = useState();
  const [rating, setRating] = useState(0);
  const [ratingInput, setRatingInput] = useState(0);
  const [showRatingForm, setShowRatingForm] = useState(false);

  const { user, isAuthenticated } = useAuthContext();

  useEffect(() => {
    if (!movieId) return;
    (async () => {
      const ratingAnalysis = await ratingService.getRatingAnalysis({ movieId, demographic: Demographic.all });
      console.log(ratingAnalysis);
      setRatingAnalysis(ratingAnalysis);
      setRating((ratingAnalysis as any)?.averageRating);
    })();
  }, []);

  const handleRate = async () => {
    if (!movieId) return;
    try {
      await ratingService.create({ rating: { movieId, rating: ratingInput } });
    } catch (error) {
      alert('You already rated this movie.');
    }

    const ratingAnalysis = await ratingService.getRatingAnalysis({ movieId, demographic: Demographic.all });
    console.log(ratingAnalysis);
    setRatingAnalysis(ratingAnalysis);
    setRating((ratingAnalysis as any)?.averageRating);

    setShowRatingForm(false);
  };

  return (
    <>
      <div className="d-flex justify-content-between">
        <RatingContainer>
          <Link className="text-decoration-none text-white" to={`/movies/${movieId}/ratings`}>
            <div>
              <RatingNumWrapper>{rating}</RatingNumWrapper> / <MaxRatingNumWrapper>5</MaxRatingNumWrapper>
            </div>
            <StarRating rating={Math.trunc(rating)} displayOnly={true} />
            <small>{(ratingAnalysis as any)?.totalRatingVoteCount} votes</small>
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
      <ReusableModal show={showRatingForm} isDark={true}>
        <>Rating movie</>
        <>
          <div className="d-flex flex-column align-items-center p-3" style={{ gap: '1rem' }}>
            <div>{(ratingAnalysis as any)?.movie?.title}</div>
            <StarRating rating={ratingInput} onRating={(rate) => setRatingInput(rate)} />
          </div>
          <div className="d-flex justify-content-end" style={{ gap: '1rem' }}>
            <button className="btn btn-secondary" onClick={(e) => setShowRatingForm(false)}>
              Close
            </button>
            <button className="btn btn-warning" type="submit" onClick={(e) => handleRate()}>
              Save
            </button>
          </div>
        </>
      </ReusableModal>
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
