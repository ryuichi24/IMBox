import React, { useEffect, useState } from 'react';
import { MovieModel } from '@IMBoxWeb/core/dist/models';
import { movieService } from '@IMBoxWeb/core/dist/services';
import { MovieItem } from '@/components/MovieItem';
import { Link } from 'react-router-dom';

export const HomePage = () => {
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
      <div className="jumbotron">
        <h1 className="display-4">IMBox</h1>
        <p className="lead">This is a movie database web service.</p>
        <hr className="my-4" />
        <p>You can explore a variety type of movies in one place.</p>
        <Link className="btn btn-warning btn-lg" to="/signin" role="button">
          Sign In
        </Link>
      </div>
      <div className="p-1 mt-5">
        <h2 className="border-start border-warning border-4 ps-2 mb-3">Top rating</h2>
        <hr />
        <div className="d-flex flex-wrap justify-content-between" style={{ gap: '1rem' }}>
          {movieList.map((movieItem) => (
            <MovieItem key={movieItem.id} movieItem={movieItem} />
          ))}
        </div>
      </div>

      <div className="p-1 mt-5">
        <h2 className="border-start border-warning border-4 ps-2 mb-3">Newly added</h2>
        <hr />
        <div className="d-flex flex-wrap justify-content-between" style={{ gap: '1rem' }}>
          {movieList.map((movieItem) => (
            <MovieItem key={movieItem.id} movieItem={movieItem} />
          ))}
        </div>
      </div>
    </>
  );
};
