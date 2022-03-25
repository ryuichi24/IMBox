import React, { useEffect, useState } from 'react';
import { MovieModel } from '@IMBoxWeb/core/dist/models';
import { movieService } from '@IMBoxWeb/core/dist/services';

export const HomePage = () => {
  const [movieList, setMovieList] = useState<MovieModel[]>([]);
  useEffect(() => {
    (async () => {
      const fetchedMovies = await movieService.get({ page: 1 });
      setMovieList(fetchedMovies);
    })();
  }, []);
  return (
    <>
      <div className="d-flex flex-wrap" style={{ gap: '1rem' }}>
        {movieList.map((movieItem) => (
          <div className="card p-2 shadow-sm w-25" key={movieItem.id}>
            {movieItem.title}
            <div className="card-body">
              <p className="text-truncate">{movieItem.description}</p>
              <p className="text-truncate">{movieItem.commentCount} comments</p>
              {movieItem.members?.map((memberItem) => (
                <div key={memberItem.id}>{memberItem.name}</div>
              ))}
            </div>
          </div>
        ))}
      </div>
    </>
  );
};
