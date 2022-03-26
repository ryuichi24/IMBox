import React, { useEffect, useState } from 'react';
import { useAuthContext } from '@/contexts/auth-context';
import { Spinner } from '@/components/Spinner';
import { CommentForm } from './CommentForm';
import { useParams } from 'react-router-dom';
import { CommentModel } from '@IMBoxWeb/core/dist/models';
import { commentService } from '@IMBoxWeb/core/dist/services';
import { CommentItem } from '@/components/CommentItem';

interface Props {}

export const CommentSection = ({}: Props) => {
  const { movieId } = useParams();
  const { isAuthenticated, isLoading } = useAuthContext();

  const [commentList, setCommentList] = useState<CommentModel[]>([]);

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

  const handleCommentFormSubmit = async (newCommentText: string) => {
    if (!movieId) return;

    const createdComment = await commentService.create({ comment: { text: newCommentText, movieId: movieId } });
    setCommentList((prev) => [...prev, createdComment]);
  };

  return (
    <>
      {isLoading ? (
        <Spinner color="yellow" />
      ) : (
        isAuthenticated && <CommentForm handleCommentFormSubmit={handleCommentFormSubmit} />
      )}
      {commentList.map((commentItem) => (
        <CommentItem key={commentItem.id} commentItem={commentItem} />
      ))}
    </>
  );
};
