import React from 'react';
import { useParams } from 'react-router-dom';

export const UserProfilePage = () => {
  const { userId } = useParams();
  return <div>UserProfilePage {userId}</div>;
};
