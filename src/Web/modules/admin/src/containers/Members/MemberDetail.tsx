import React, { useEffect, useState } from 'react';
import { MemberModel } from '@IMBoxWeb/core/dist/models';
import { memberService } from '@IMBoxWeb/core/dist/services';
import { useNavigate, useParams } from 'react-router-dom';
import { InputField } from '@/components/InputField';
import { PrimaryBtn, SecondaryBtn } from '@/components/UI';

export const MemberDetail = () => {
  const { memberId } = useParams();
  const navigate = useNavigate();

  const [memberName, setMemberName] = useState('');
  const [memberDescription, setMemberDescription] = useState('');
  const [memberImage, setMemberImage] = useState('');
  const [memberBirthDate, setMemberBirthDate] = useState('');
  const [memberRole, setMemberRole] = useState('label');

  const setInputs = (member: MemberModel) => {
    setMemberName(member.name);
    setMemberDescription(member.description);
    setMemberImage(member.headshotUrl);
    setMemberBirthDate(member.birthDate.toString().split('T')[0]);
    setMemberRole(member.role);
  };

  useEffect(() => {
    if (!memberId) return;

    (async () => {
      try {
        const fetchedMember = await memberService.getById({ memberId });
        setInputs(fetchedMember);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, [memberId]);

  const handleMemberFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    try {
      if (!memberId) return;
      e.preventDefault();

      const memberToUpdate: MemberModel = {
        name: memberName,
        description: memberDescription,
        headshotUrl: memberImage,
        birthDate: new Date(memberBirthDate).toISOString(),
        role: memberRole,
      };

      await memberService.update({ member: memberToUpdate, memberId: memberId });

      alert('successfully updated');
    } catch (error) {
      console.error(error);
      alert((error as any)?.response?.data || (error as any).message);
    }
  };

  const handleDelete = async () => {
    if (!memberId) return;
    const confirmed = confirm('Are you sure you want to delete it?');
    if (!confirmed) return;
    try {
      await memberService.remove({ memberId });
      alert('Successfully removed');
      navigate('/admin/members');
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
        <div className="card h-100 p-4">
          <div className="d-flex justify-content-end">
            <button className="btn btn-danger" style={{ width: '80px' }} onClick={handleDelete}>
              Delete
            </button>
          </div>
          <form onSubmit={handleMemberFormSubmit}>
            <div className="mb-3">
              <InputField
                label="Name"
                id="memberName"
                type="text"
                onChange={(e) => setMemberName(e.target.value)}
                value={memberName}
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
              />
            </div>

            <div className="mb-3">
              <InputField
                label="Birth date"
                id="memberBirthDate"
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
              <SecondaryBtn btnText="Close" onClick={() => navigate('/admin/members')} />
              <PrimaryBtn btnText="Save" />
            </div>
          </form>
        </div>
      </div>
    </>
  );
};
