import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { movieService } from '@IMBoxWeb/core/dist/services';
import { MovieModel } from '@IMBoxWeb/core/dist/models';
import { MovieDetail } from '@/containers/MovieDetail';

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

  return <MovieDetail movieItem={movieItem} />;
};
