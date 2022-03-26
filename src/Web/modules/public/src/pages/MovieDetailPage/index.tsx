import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { commentService, movieService } from '@IMBoxWeb/core/dist/services';
import { CommentModel, MovieModel } from '@IMBoxWeb/core/dist/models';
import { Heading } from '@/components/UI';
import { CommentItem } from '@/components/CommentItem';

export const MovieDetailPage = () => {
  const { movieId } = useParams();
  const [movieItem, setMovieItem] = useState<MovieModel>();
  const [commentList, setCommentList] = useState<CommentModel[]>([]);

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

  useEffect(() => {
    if (!movieId) return;

    (async () => {
      try {
        const fetchedComments = await commentService.get({ movieId, page: 1 });
        setCommentList(fetchedComments);
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
            </div>
            <div className="d-flex justify-content-between pt-2">
              <div></div>
            </div>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <Heading level={2} text="Comments" />
        {commentList.map((commentItem) => (
          <CommentItem key={commentItem.id} commentItem={commentItem} />
        ))}
      </div>
    </div>
  );
};
