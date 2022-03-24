import React from 'react';
import { Link } from 'react-router-dom';
import { UserModel } from '@/models/user-model';
import { SecondaryBtn } from '../UI';

interface Props {
  userItem: UserModel;
}

export const UserItem = ({ userItem }: Props) => {
  return (
    <tr key={userItem.id}>
      <td>{userItem.username}</td>
      <td>{userItem.email}</td>
      <td>{userItem.gender}</td>
      <td>{userItem.country}</td>
      <td>{userItem.birthDate?.toString().split('T')[0]}</td>
      <td>
        {userItem?.roles?.map((role) => (
          <span key={role}>{role} </span>
        ))}
      </td>
      <td>
        <Link to={`/users/${userItem.id}`}>
          <SecondaryBtn btnText="Edit" />
        </Link>
      </td>
    </tr>
  );
};
