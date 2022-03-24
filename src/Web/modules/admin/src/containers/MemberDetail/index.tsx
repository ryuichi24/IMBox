import React, { useEffect, useState } from 'react';
import { memberService } from '@/services/member-service';
import { useParams } from 'react-router-dom';
import { MemberModel } from '@/models/member-model';

export const MemberDetail = () => {
  const { memberId } = useParams();
  const [member, setMember] = useState<MemberModel>();

  const [memberName, setMemberName] = useState('');
  const [memberDescription, setMemberDescription] = useState('');
  const [memberImage, setMemberImage] = useState('');
  const [memberBirthDate, setMemberBirthDate] = useState('');
  const [memberRole, setMemberRole] = useState('label');

  const clearInputs = () => {
    setMemberName('');
    setMemberDescription('');
    setMemberImage('');
    setMemberBirthDate('');
    setMemberRole('label');
  };

  useEffect(() => {
    if (!memberId) return;

    (async () => {
      try {
        const fetchedMember = await memberService.getById({ memberId });
        setMember(fetchedMember);
      } catch (error) {
        console.error(error);
        alert((error as any).response.data);
      }
    })();
  }, [memberId]);
  return (
    <>
      <div>{member?.name}</div>
      <div>{member?.description}</div>
      <div>{member?.birthDate}</div>
      <div>{member?.role}</div>
      <img src={member?.headshotUrl} alt={member?.name} />
    </>
  );
};
