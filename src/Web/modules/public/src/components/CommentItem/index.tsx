import React from 'react';
import { CommentModel } from '@IMBoxWeb/core/dist/models';
import { stringToColor } from '@IMBoxWeb/core/dist/util';
import { CircleIconWrapper } from '../UI/CircleIconWrapper';

interface Props {
  commentItem: CommentModel;
}

export const CommentItem = ({ commentItem }: Props) => {
  return (
    <div key={commentItem.id}>
      <div className="d-flex" style={{ minHeight: '60px' }}>
        <div id="commenterIcon" style={{ flex: '0 0 60px', padding: '0.5rem' }}>
          <CircleIconWrapper color={stringToColor(commentItem.commenter?.id || '')} size={45}>
            {commentItem?.commenter?.name[0].toUpperCase()}
          </CircleIconWrapper>
        </div>
        <div className="p-2" style={{ flex: '1 1 auto', minWidth: '0' }}>
          <div id="commentHeading" className="d-flex" style={{ gap: '0.5rem' }}>
            <div id="commenterName">{commentItem.commenter?.name}</div>
            <div id="commentedAt" style={{ color: '#a0a0a0' }}>
              {commentItem.createdAt?.split('T')[0]}
            </div>
          </div>
          <div id="commentBody" style={{}}>
            {commentItem.text}
          </div>
        </div>
      </div>
    </div>
  );
};
