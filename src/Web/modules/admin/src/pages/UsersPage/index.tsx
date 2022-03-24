import { UserModel } from '@/models/user-model';
import { userService } from '@/services/user-service';
import React, { useEffect, useState } from 'react';

export const UsersPage = () => {
  const [userList, setUserList] = useState<UserModel[]>([]);
  useEffect(() => {
    (async () => {
      const users = await userService.get({ page: 1 });
      setUserList(users);
    })();
  }, []);

  return (
    <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
      <div className="mb-3 d-flex w-100 justify-content-between">
        <h2>Users</h2>
        <button className="btn btn-primary">+ New user</button>
      </div>
      <div className="card h-100">
        <table className="table">
          <thead>
            <tr>
              <th scope="col">Username</th>
              <th scope="col">Email</th>
              <th scope="col">Gender</th>
              <th scope="col">Country</th>
              <th scope="col">Birth date</th>
              <th scope="col">Role</th>
            </tr>
          </thead>
          <tbody>
            {userList.map((userItem) => (
              <tr key={userItem.id}>
                <td>{userItem.username}</td>
                <td>{userItem.email}</td>
                <td>{userItem.gender}</td>
                <td>{userItem.country}</td>
                <td>{userItem.birthDate.toString().split('T')[0]}</td>
                <td>
                  {userItem.roles.map((role) => (
                    <span key={role}>{role} </span>
                  ))}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
