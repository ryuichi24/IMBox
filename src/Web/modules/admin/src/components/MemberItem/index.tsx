import React from 'react';
import { MemberModel } from '@/models/member-model';

interface Props {
  memberItem: MemberModel;
}

export const MemberItem = ({ memberItem }: Props) => {
  return (
    <div key={memberItem.id} className="card shadow-sm h-auto" style={{ maxWidth: '230px', maxHeight: '470px' }}>
      <img src={memberItem.headshotUrl} alt={memberItem.name} className="w-100" />
      <div className="card-body">
        <p className="fw-bold text-center">{memberItem.name}</p>
        <p className="text-truncate">{memberItem.description}</p>
        <small className="d-block">Role: {memberItem.role}</small>
        <small className="d-block">Birth Date: {memberItem.birthDate.toString().split('T')[0]}</small>
      </div>
    </div>
  );
};
