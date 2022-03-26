import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { commentService, movieService } from '@IMBoxWeb/core/dist/services';
import { CommentModel, MovieModel } from '@IMBoxWeb/core/dist/models';
import { Heading } from '@/components/UI';
import { CommentItem } from '@/components/CommentItem';
import { CircleIconWrapper } from '@/components/UI/CircleIconWrapper';
import { useAuthContext } from '@/contexts/auth-context';
import { Spinner } from '@/components/Spinner';

export const MovieDetailPage = () => {
  const { movieId } = useParams();
  const { isAuthenticated, isLoading } = useAuthContext();

  const [movieItem, setMovieItem] = useState<MovieModel>();
  const [commentList, setCommentList] = useState<CommentModel[]>([]);
  const [commentText, setCommentText] = useState('');

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

  const handleCommentFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    if (!movieId) return;
    e.preventDefault();

    const createdComment = await commentService.create({ comment: { text: commentText, movieId: movieId } });
    setCommentList((prev) => [...prev, createdComment]);
    setCommentText('');
  };

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
        {isLoading ? (
          <Spinner color="yellow" />
        ) : (
          isAuthenticated && (
            <div id="commentForm" className="d-flex">
              <div style={{ flex: '0 0 60px', padding: '0.5rem' }}>
                <CircleIconWrapper size={40}>{'R'}</CircleIconWrapper>
              </div>
              <form className="p-2" style={{ flex: '1 1 auto', minWidth: '0' }} onSubmit={handleCommentFormSubmit}>
                <div className="d-flex flex-column" style={{ gap: '0.5rem' }}>
                  <textarea
                    id="movieComment"
                    rows={1}
                    onChange={(e) => setCommentText(e.target.value)}
                    value={commentText}
                    placeholder="Add a comment"
                    style={{
                      outline: 'none',
                      backgroundColor: 'black',
                      border: 'none',
                      borderBottom: '1px solid grey',
                      color: 'whitesmoke',
                      width: '100%',
                    }}
                  />
                  <div className="d-flex justify-content-end">
                    <button
                      style={{
                        outline: 'none',
                        backgroundColor: 'black',
                        border: 'none',
                        color: '#bebebe',
                      }}
                    >
                      CANCEL
                    </button>
                    <button
                      style={{
                        outline: 'none',
                        backgroundColor: 'black',
                        border: 'none',
                        color: '#bebebe',
                      }}
                    >
                      COMMENT
                    </button>
                  </div>
                </div>
              </form>
            </div>
          )
        )}
        {commentList.map((commentItem) => (
          <CommentItem key={commentItem.id} commentItem={commentItem} />
        ))}
      </div>
    </div>
  );
};
