import React, { useState } from 'react';
import { CircleIconWrapper } from '@/components/UI/CircleIconWrapper';

interface Props {
  handleCommentFormSubmit: (newCommentText: string) => Promise<void>;
}

export const CommentForm = ({ handleCommentFormSubmit }: Props) => {
  const [commentText, setCommentText] = useState('');

  const handleSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    e.preventDefault();
    await handleCommentFormSubmit(commentText);
    setCommentText('');
  };

  return (
    <div id="commentForm" className="d-flex">
      <div style={{ flex: '0 0 60px', padding: '0.5rem' }}>
        <CircleIconWrapper size={40}>{'R'}</CircleIconWrapper>
      </div>
      <form className="p-2" style={{ flex: '1 1 auto', minWidth: '0' }} onSubmit={handleSubmit}>
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
  );
};
