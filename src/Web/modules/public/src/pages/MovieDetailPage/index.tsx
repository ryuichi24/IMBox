import React, { useEffect, useState } from 'react';
import { movieService } from '@IMBoxWeb/core/dist/services';
import { useParams } from 'react-router-dom';
import { MovieModel } from '@IMBoxWeb/core/src/models';
import { Link } from 'react-router-dom';
import { Heading } from '@/components/UI';

export const MovieDetailPage = () => {
  const { movieId } = useParams();
  const [movieItem, setMovieItem] = useState<MovieModel>();

  useEffect(() => {
    if (!movieId) return;

    (async () => {
      try {
        const fetchedMovie = await movieService.getById({ movieId });
        setMovieItem(fetchedMovie);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, [movieId]);
  return (
    <div className="shadow-sm h-auto">
      <h2 className="display-4 fw-bold mb-3">{movieItem?.title}</h2>
      <div className="d-flex" style={{ gap: '2rem' }}>
        <img
          src={movieItem?.mainPosterUrl}
          alt={movieItem?.title}
          className="w-auto d-block"
          style={{ maxHeight: '800px' }}
        />
        <div>
          <div>
            <Heading level={3} text="Summary" />
            <p>{movieItem?.description}</p>
          </div>
          <div>
            <Heading level={3} text="Members" />
            <h4>Directors</h4>
            <div>
              {movieItem?.directors?.map((memberItem) => (
                <Link className="me-2" key={memberItem.id} to={`/members/${memberItem.id}`}>
                  {memberItem?.name}
                </Link>
              ))}
            </div>
            <hr />
            <h4>Writers</h4>
            <div>
              {movieItem?.writers?.map((memberItem) => (
                <Link className="me-2" key={memberItem.id} to={`/members/${memberItem.id}`}>
                  {memberItem?.name}
                </Link>
              ))}
            </div>
            <hr />
            <h4>Casts</h4>
            <div>
              {movieItem?.casts?.map((memberItem) => (
                <Link className="me-2" key={memberItem.id} to={`/members/${memberItem.id}`}>
                  {memberItem?.name}
                </Link>
              ))}
            </div>
            <hr />
          </div>
          <div>
            <Heading level={3} text="Rating" />
          </div>
          <div className="d-flex justify-content-between pt-2">
            <div></div>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <Heading level={2} text="Comments" />
      </div>
    </div>
  );
};
