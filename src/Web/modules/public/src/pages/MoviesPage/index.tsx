import React, { useEffect, useState } from 'react';
import { MovieModel } from '@IMBoxWeb/core/dist/models';
import { movieService } from '@IMBoxWeb/core/dist/services';
import { MovieItem } from '@/components/MovieItem';
import { Heading } from '@/components/UI';

export const MoviesPage = () => {
  const [movieList, setMovieList] = useState<MovieModel[]>([]);
  useEffect(() => {
    (async () => {
      try {
        const fetchedMovies = await movieService.get({ page: 1 });
        setMovieList(fetchedMovies);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, []);
  return (
    <>
      <Heading level={1} text="Movies" />
      <div className="d-flex flex-wrap justify-content-between" style={{ gap: '1rem' }}>
        {movieList.map((movieItem) => (
          <MovieItem key={movieItem.id} movieItem={movieItem} />
        ))}
      </div>
    </>
  );
};
