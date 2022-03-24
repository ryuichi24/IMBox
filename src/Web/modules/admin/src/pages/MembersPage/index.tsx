import React, { useEffect, useState } from 'react';
import { MemberModel } from '@/models/member-model';
import { memberService } from '@/services/member-service';
import { ReusableModal } from '@/components/ReusableModal';
import { MemberItem } from '@/components/MemberItem';
import { PrimaryBtn, SecondaryBtn } from '@/components/UI';

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
          <PrimaryBtn btnText="+ New member" onClick={(e) => setShowNewFormModal(true)} />
        </div>
        <div className="card h-100 d-flex flex-row" style={{ gap: '1rem' }}>
          {memberList.map((memberItem) => (
            <MemberItem key={memberItem.id} memberItem={memberItem} />
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
              <SecondaryBtn btnText="Close" onClick={closeNewFormModal} />
              <PrimaryBtn btnText="Save" />
            </div>
          </form>
        </>
      </ReusableModal>
    </>
  );
};
