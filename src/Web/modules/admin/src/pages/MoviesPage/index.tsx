import React, { useEffect, useState } from 'react';
import { MovieModel } from '@/models/movie-model';
import { movieService } from '@/services/movie-service';

export const MoviesPaage = () => {
  const [movieList, setMovieList] = useState<MovieModel[]>([]);
  useEffect(() => {
    (async () => {
      try {
        const movies = await movieService.get({ page: 1 });
        setMovieList(movies);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, []);

  return (
    <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
      <div className="mb-3 d-flex w-100 justify-content-between">
        <h2>Movies</h2>
        <button className="btn btn-primary">+ New movie</button>
      </div>
      <div className="card h-100 d-flex flex-row" style={{ gap: '1rem' }}>
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
    </div>
  );
};
