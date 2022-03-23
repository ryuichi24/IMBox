import React, { useEffect, useState } from 'react';
import { MemberModel } from '@/models/member-model';
import { memberService } from '@/services/member-service';

export const MembersPage = () => {
  const [memberList, setMemberList] = useState<MemberModel[]>([]);
  useEffect(() => {
    (async () => {
      const members = await memberService.get({ page: 1 });
      setMemberList(members);
    })();
  }, []);

  return (
    <div className="card h-100 m-4 d-flex flex-row" style={{ gap: '1rem' }}>
      {memberList.map((memberItem) => (
        <div key={memberItem.id} className="card shadow-sm h-auto" style={{ maxWidth: '230px', maxHeight: '470px' }}>
          <img src={memberItem.headshotUrl} alt={memberItem.name} className="w-100" />
          <div className="card-body">
            <p className="fw-bold text-center">{memberItem.name}</p>
            <p className="text-truncate">{memberItem.description}</p>
            <small>Birth Date: {memberItem.birthDate.toString().split('T')[0]}</small>
          </div>
        </div>
      ))}
    </div>
  );
};
