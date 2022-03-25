import React from 'react';
import { useParams } from 'react-router-dom';

export const MemberDetailPage = () => {
  const { memberId } = useParams();
  return <div>MemberDetailPage {memberId}</div>;
};
