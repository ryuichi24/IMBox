import React from 'react';
import { MovieModel } from '@IMBoxWeb/core/dist/models';
import { Link } from 'react-router-dom';
import { SecondaryBtn } from '../UI';

interface Props {
  movieItem: MovieModel;
}

export const MovieItem = ({ movieItem }: Props) => {
  return (
    <div key={movieItem.id} className="card shadow-sm h-auto" style={{ maxWidth: '230px', maxHeight: '500px' }}>
      <img src={movieItem.mainPosterUrl} alt={movieItem.title} className="w-100" />
      <div className="card-body">
        <p className="fw-bold text-center">{movieItem.title}</p>
        <p className="text-truncate">{movieItem.description}</p>

        <div className="d-flex justify-content-between pt-2">
          <div>
            <small>{movieItem.commentCount} comments</small>
          </div>
          <div>
            <Link to={`/admin/movies/${movieItem.id}`}>
              <SecondaryBtn btnText="Edit" />
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};
