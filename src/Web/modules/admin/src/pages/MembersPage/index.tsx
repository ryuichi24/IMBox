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
      <ReusableModal show={showNewFormModal}>
        <>New Member</>
        <>
          <form>
            <div className="mb-3">
              <label htmlFor="memberName" className="form-label">
                Name
              </label>
              <input type="email" className="form-control" id="memberName" />
            </div>

            <div className="mb-3">
              <label htmlFor="memberDescription" className="form-label">
                Description
              </label>
              <textarea className="form-control" id="memberDescription" rows={3}></textarea>
            </div>

            <div className="mb-3">
              <label htmlFor="memberImage" className="form-label">
                Image
              </label>
              <input type="url" className="form-control" id="memberImage" placeholder="https//example.com/image.jpeg" />
              <div id="emailHelp" className="form-text">
                Please paste URL of the image
              </div>
            </div>

            <div className="mb-3">
              <label htmlFor="memberBirthDate" className="form-label">
                Birth date
              </label>
              <input id="memberBirthDate" className="form-control" type="date" />
            </div>

            <div className="mb-3">
              <label htmlFor="memberRole" className="form-label">
                Role
              </label>
              <select id="memberRole" className="form-select">
                <option selected>--- Member role ---</option>
                <option value="cast">Cast</option>
                <option value="director">Director</option>
                <option value="writer">Writer</option>
              </select>
            </div>

            <div className="d-flex justify-content-end" style={{ gap: '1rem' }}>
              <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" onClick={closeNewFormModal}>
                Close
              </button>
              <button type="button" className="btn btn-primary">
                Save changes
              </button>
            </div>
          </form>
        </>
      </ReusableModal>
    </>
  );
};
