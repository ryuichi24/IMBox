import React from 'react';
import { MemberModel } from '@IMBoxWeb/core/dist/models';
import { SecondaryBtn } from '../UI';
import { Link } from 'react-router-dom';

interface Props {
  memberItem: MemberModel;
}

export const MemberItem = ({ memberItem }: Props) => {
  return (
    <div key={memberItem.id} className="card shadow-sm h-auto" style={{ maxWidth: '300px', maxHeight: '550px' }}>
      <div
        className="w-100 h-75 d-flex justify-content-center align-items-center"
        style={{ backgroundColor: 'whitesmoke' }}
      >
        <div>
          <img src={memberItem.headshotUrl} alt={memberItem.name} className="w-100" />
        </div>
      </div>
      <div className="card-body h-25">
        <p className="fw-bold text-center m-1">{memberItem.name}</p>
        <p className="text-truncate m-1">{memberItem.description}</p>
        <div className="d-flex justify-content-between pt-2">
          <div>
            <small className="d-block">Role: {memberItem.role}</small>
            <small className="d-block">Birth Date: {memberItem.birthDate.toString().split('T')[0]}</small>
          </div>

          <div>
            <Link to={`/members/${memberItem.id}`}>
              <SecondaryBtn btnText="Edit" />
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};
