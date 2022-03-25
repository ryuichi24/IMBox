import React from 'react';
import { Link } from 'react-router-dom';
import { MovieModel } from '@IMBoxWeb/core/src/models';

interface Props {
  movieItem: MovieModel;
}

export const MovieItem = ({ movieItem }: Props) => {
  return (
    <div
      className="card p-2 shadow-sm w-25"
      style={{ backgroundColor: '#1a1a1a', minHeight: '400px' }}
      key={movieItem.id}
    >
      <h3>{movieItem.title}</h3>
      <Link to={`/movies/${movieItem.id}`}>
        <img src={movieItem.mainPosterUrl} alt={movieItem.title} className="w-100" />
      </Link>
      <div className="card-body">
        <p className="text-truncate">{movieItem.description}</p>
        <p className="text-truncate">{movieItem.commentCount} comments</p>
        {movieItem.members?.map((memberItem) => (
          <div key={memberItem.id}>{memberItem.name}</div>
        ))}
      </div>
    </div>
  );
};
