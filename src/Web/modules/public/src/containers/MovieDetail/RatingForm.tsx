import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { ratingService } from '@IMBoxWeb/core/dist/services';
import { ReusableModal } from '@/components/ReusableModal';
import { StarRating } from '@/components/StarRating';
import { useAuthContext } from '@/contexts/auth-context';

interface Props {
  handleClose: any;
  ratingAnalytics: any;
  handleRate: any;
}

export const RatingForm = ({ ratingAnalytics, handleClose, handleRate }: Props) => {
  const { movieId } = useParams();
  const { user, isAuthenticated } = useAuthContext();

  const [ratingInput, setRatingInput] = useState(0);
  const [userRating, setUserRating] = useState<any>();
  const [userAlreadyRated, setUserAlreadyRated] = useState(false);

  useEffect(() => {
    if (!user) return;

    (async () => {
      const fetchedUserRatings = await ratingService.getUsesRatings({ raterId: user.id });

      const userRatingForThisMovie = fetchedUserRatings.ratings.find(
        (ratingItem: any) => ratingItem.movie.id === movieId,
      );
      setUserRating(userRatingForThisMovie);
      setUserAlreadyRated(!!userRatingForThisMovie);

      if (!!userRatingForThisMovie) {
        setRatingInput(userRatingForThisMovie.rating);
      }
    })();
  }, [user]);

  const handleRateSave: React.MouseEventHandler<HTMLButtonElement> = async (e) => {
    if (!movieId) return;
    try {
      await ratingService.create({ rating: { movieId, rating: ratingInput } });
    } catch (error) {
      alert('You already rated this movie.');
      setRatingInput(0);
    }

    await handleRate();

    setRatingInput(0);
    handleClose();
  };

  const handleRateUpdate: React.MouseEventHandler<HTMLButtonElement> = async (e) => {
    if (!movieId) return;
    try {
      await ratingService.update({ rating: { movieId, rating: ratingInput }, ratingId: userRating?.id });
    } catch (error) {
      alert((error as any).response.data);
    }

    await handleRate();
    handleClose();
  };

  const handleRateRemove: React.MouseEventHandler<HTMLButtonElement> = async (e) => {
    if (!movieId) return;
    try {
      console.log(userRating);
      await ratingService.remove({ ratingId: userRating.id });
    } catch (error) {
      alert((error as any).response.data);
    }

    await handleRate();

    setRatingInput(0);
    handleClose();
  };

  return (
    <ReusableModal show={true} isDark={true}>
      <>Rating movie</>
      <>
        <div className="d-flex flex-column align-items-center p-3" style={{ gap: '1rem' }}>
          <div>{ratingAnalytics?.movie?.title}</div>
          <StarRating rating={ratingInput} onRating={(rate) => setRatingInput(rate)} />
        </div>
        <div className="d-flex flex-column mt-5" style={{ gap: '1rem' }}>
          {!userAlreadyRated && (
            <button className="btn btn-warning" onClick={handleRateSave}>
              Save
            </button>
          )}

          {userAlreadyRated && (
            <button className="btn btn-warning" onClick={handleRateUpdate}>
              Update
            </button>
          )}

          {userAlreadyRated && (
            <button className="btn btn-danger" onClick={handleRateRemove}>
              Remove
            </button>
          )}

          <button
            className="btn btn-secondary"
            onClick={(e) => {
              setRatingInput(0);
              handleClose();
            }}
          >
            Close
          </button>
        </div>
      </>
    </ReusableModal>
  );
};
