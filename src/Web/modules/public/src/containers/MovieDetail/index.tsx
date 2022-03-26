import React from 'react';
import { Heading } from '@/components/UI';
import { Link } from 'react-router-dom';
import { MovieModel } from '@IMBoxWeb/core/src/models';
import { CommentSection } from './CommentSection';
import { RatingSection } from './RatingSection';

interface Props {
  movieItem?: MovieModel;
}

export const MovieDetail = ({ movieItem }: Props) => {
  return (
    <div className="shadow-sm h-auto">
      <h2 className="display-4 fw-bold mb-3">{movieItem?.title}</h2>
      <div className="d-flex" style={{ gap: '2rem' }}>
        <img
          src={movieItem?.mainPosterUrl}
          alt={movieItem?.title}
          className="w-auto d-block"
          style={{ maxHeight: '700px' }}
        />
        <div>
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
              <RatingSection />
            </div>
            <div className="d-flex justify-content-between pt-2">
              <div></div>
            </div>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <Heading level={2} text="Comments" />
        <CommentSection />
      </div>
    </div>
  );
};
