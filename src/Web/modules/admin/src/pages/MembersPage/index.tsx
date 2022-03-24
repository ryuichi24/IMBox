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

  const [memberName, setMemberName] = useState('');
  const [memberDescription, setMemberDescription] = useState('');
  const [memberImage, setMemberImage] = useState('');
  const [memberBirthDate, setMemberBirthDate] = useState('');
  const [memberRole, setMemberRole] = useState('label');

  const handleNewMemberFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    e.preventDefault();
    const newMember: MemberModel = {
      name: memberName,
      description: memberDescription,
      headshotUrl: memberImage,
      birthDate: new Date(memberBirthDate).toISOString(),
      role: memberRole,
    };

    setMemberList((prev) => [...prev, newMember]);

    await memberService.create({ member: newMember });

    clearInputs();
  };

  const clearInputs = () => {
    setMemberName('');
    setMemberDescription('');
    setMemberImage('');
    setMemberBirthDate('');
    setMemberRole('label');
  };

  return (
    <>
      <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
        <div className="mb-3 d-flex w-100 justify-content-between">
          <h2>Members</h2>
          <PrimaryBtn btnText="+ New member" onClick={(e) => setShowNewFormModal(true)} />
        </div>
        <div className="card h-100 d-flex flex-row flex-wrap" style={{ gap: '1rem' }}>
          {memberList.map((memberItem) => (
            <MemberItem key={memberItem.id} memberItem={memberItem} />
          ))}
        </div>
      </div>
      <ReusableModal show={showNewFormModal}>
        <>New Member</>
        <>
          <form onSubmit={handleNewMemberFormSubmit}>
            <div className="mb-3">
              <label htmlFor="memberName" className="form-label">
                Name
              </label>
              <input
                type="text"
                className="form-control"
                id="memberName"
                onChange={(e) => setMemberName(e.target.value)}
                value={memberName}
              />
            </div>

            <div className="mb-3">
              <label htmlFor="memberDescription" className="form-label">
                Description
              </label>
              <textarea
                className="form-control"
                id="memberDescription"
                rows={3}
                value={memberDescription}
                onChange={(e) => setMemberDescription(e.target.value)}
              ></textarea>
            </div>

            <div className="mb-3">
              <label htmlFor="memberImage" className="form-label">
                Image
              </label>
              <input
                type="url"
                className="form-control"
                id="memberImage"
                placeholder="https//example.com/image.jpeg"
                value={memberImage}
                onChange={(e) => setMemberImage(e.target.value)}
              />
              <div id="emailHelp" className="form-text">
                Please paste URL of the image
              </div>
            </div>

            <div className="mb-3">
              <label htmlFor="memberBirthDate" className="form-label">
                Birth date
              </label>
              <input
                id="memberBirthDate"
                className="form-control"
                type="date"
                onChange={(e) => setMemberBirthDate(e.target.value)}
                value={memberBirthDate}
              />
            </div>

            <div className="mb-3">
              <label htmlFor="memberRole" className="form-label">
                Role
              </label>
              <select
                id="memberRole"
                className="form-select"
                value={memberRole}
                onChange={(e) => setMemberRole(e.target.value)}
              >
                <option value="label">--- Member role ---</option>
                <option value="cast">Cast</option>
                <option value="director">Director</option>
                <option value="writer">Writer</option>
              </select>
            </div>

            <div className="d-flex justify-content-end" style={{ gap: '1rem' }}>
              <SecondaryBtn btnText="Close" onClick={closeNewFormModal} type="submit" />
              <PrimaryBtn btnText="Save" />
            </div>
          </form>
        </>
      </ReusableModal>
    </>
  );
};
