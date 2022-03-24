import React, { useEffect, useState } from 'react';
import { MemberModel } from '@/models/member-model';
import { memberService } from '@/services/member-service';
import { ReusableModal } from '@/components/ReusableModal';

export const MembersPage = () => {
  const [showNewFormModal, setShowNewFormModal] = useState(false);
  const closeNewFormModal = () => {
    setShowNewFormModal(false);
  };

  const [memberList, setMemberList] = useState<MemberModel[]>([]);
  useEffect(() => {
    (async () => {
      const members = await memberService.get({ page: 1 });
      setMemberList(members);
    })();
  }, []);

  return (
    <>
      <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
        <div className="mb-3 d-flex w-100 justify-content-between">
          <h2>Members</h2>
          <button className="btn btn-primary" onClick={(e) => setShowNewFormModal(true)}>
            + New member
          </button>
        </div>
        <div className="card h-100 d-flex flex-row" style={{ gap: '1rem' }}>
          {memberList.map((memberItem) => (
            <div
              key={memberItem.id}
              className="card shadow-sm h-auto"
              style={{ maxWidth: '230px', maxHeight: '470px' }}
            >
              <img src={memberItem.headshotUrl} alt={memberItem.name} className="w-100" />
              <div className="card-body">
                <p className="fw-bold text-center">{memberItem.name}</p>
                <p className="text-truncate">{memberItem.description}</p>
                <small>Birth Date: {memberItem.birthDate.toString().split('T')[0]}</small>
              </div>
            </div>
          ))}
        </div>
      </div>
      <ReusableModal show={showNewFormModal} onClose={closeNewFormModal}>
        <>New Member</>
        <>some inputs</>
        <>
          <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" onClick={closeNewFormModal}>
            Close
          </button>
          <button type="button" className="btn btn-primary">
            Save changes
          </button>
        </>
      </ReusableModal>
    </>
  );
};
