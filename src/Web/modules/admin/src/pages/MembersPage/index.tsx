import React, { useEffect, useState } from 'react';
import { MemberModel } from '@IMBoxWeb/core/dist/models';
import { memberService } from '@IMBoxWeb/core/dist/services';
import { ReusableModal } from '@/components/ReusableModal';
import { MemberItem } from '@/components/MemberItem';
import { PrimaryBtn, SecondaryBtn } from '@/components/UI';
import { InputField } from '@/components/InputField';

export const MembersPage = () => {
  const [showNewFormModal, setShowNewFormModal] = useState(false);
  const closeNewFormModal = () => {
    setShowNewFormModal(false);
  };

  const [memberList, setMemberList] = useState<MemberModel[]>([]);
  useEffect(() => {
    (async () => {
      try {
        const members = await memberService.get({ page: 1 });
        setMemberList(members);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, []);

  const [memberName, setMemberName] = useState('');
  const [memberDescription, setMemberDescription] = useState('');
  const [memberImage, setMemberImage] = useState('');
  const [memberBirthDate, setMemberBirthDate] = useState('');
  const [memberRole, setMemberRole] = useState('label');

  const handleNewMemberFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    try {
      e.preventDefault();
      const newMember: MemberModel = {
        name: memberName,
        description: memberDescription,
        headshotUrl: memberImage,
        birthDate: new Date(memberBirthDate).toISOString(),
        role: memberRole,
      };

      const createdUser = await memberService.create({ member: newMember });

      alert('successfully created');

      setMemberList((prev) => [...prev, { ...createdUser }]);
      clearInputs();
    } catch (error) {
      console.error(error);
      alert((error as any)?.response?.data || (error as any).message);
    }
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
              <InputField
                label="Name"
                id="memberName"
                type="text"
                onChange={(e) => setMemberName(e.target.value)}
                value={memberName}
                required
              />
            </div>

            <div className="mb-3">
              <InputField
                multiLine
                maxRows={3}
                label="Description"
                id="memberDescription"
                onChange={(e) => setMemberDescription(e.target.value)}
                value={memberDescription}
                required
              />
            </div>

            <div className="mb-3">
              <InputField
                label="Image"
                id="memberImage"
                placeholder="https//example.com/image.jpeg"
                formText="Please paste URL of the image"
                type="url"
                onChange={(e) => setMemberImage(e.target.value)}
                value={memberImage}
                required
              />
            </div>

            <div className="mb-3">
              <InputField
                label="Birth date"
                id="memberBirthDate"
                type="date"
                onChange={(e) => setMemberBirthDate(e.target.value)}
                value={memberBirthDate}
                required
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
                required
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
