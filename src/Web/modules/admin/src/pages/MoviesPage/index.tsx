import { MovieModel } from '@/models/movie-model';
import { movieService } from '@/services/movie-service';
import React, { useEffect, useState } from 'react';

export const MoviesPaage = () => {
  const [movieList, setMovieList] = useState<MovieModel[]>([]);
  useEffect(() => {
    (async () => {
      const movies = await movieService.get({ page: 1 });
      setMovieList(movies);
    })();
  }, []);

  return (
    <div className="card h-100 m-4 d-flex flex-row" style={{ gap: '1rem' }}>
      {movieList.map((movieItem) => (
        <div key={movieItem.id} className="card shadow-sm h-auto" style={{ maxWidth: '230px', maxHeight: '470px' }}>
          <img src={movieItem.mainPosterUrl} alt={movieItem.title} className="w-100" />
          <div className="card-body">
            <p className="fw-bold text-center">{movieItem.title}</p>
            <p className="text-truncate">{movieItem.description}</p>
            <small>{movieItem.commentCount} comments</small>
          </div>
        </div>
      ))}
    </div>
  );
};
