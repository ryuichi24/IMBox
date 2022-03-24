import React, { useEffect, useState } from 'react';
import { UserModel } from '@/models/user-model';
import { userService } from '@/services/user-service';
import { ReusableModal } from '@/components/ReusableModal';
import { PrimaryBtn, SecondaryBtn } from '@/components/UI';
import { InputField } from '@/components/InputField';
import { countries } from '@/utils/countries';
import { UserItem } from '@/components/UserItem';

export const UsersPage = () => {
  const [userList, setUserList] = useState<UserModel[]>([]);

  const [showFormModal, setShowFormModal] = useState(false);
  const closeNewFormModal = () => {
    setShowFormModal(false);
  };

  const [userUsername, setUserUsername] = useState('');
  const [userEmail, setUserEmail] = useState('');
  const [userPassword, setUserPassword] = useState('');
  const [userBirthDate, setUserBirthDate] = useState('');
  const [userGender, setUserGender] = useState('');
  const [userCountry, setUserCountry] = useState('');
  const [userRoles, setUserRoles] = useState<string[]>([]);

  const clearInputs = () => {
    setUserUsername('');
    setUserEmail('');
    setUserPassword('');
    setUserBirthDate('');
    setUserGender('');
    setUserCountry('');
    setUserRoles([]);
  };

  useEffect(() => {
    (async () => {
      try {
        const users = await userService.get({ page: 1 });
        setUserList(users);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, []);

  const handleFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    try {
      e.preventDefault();

      const newUser: UserModel = {
        username: userUsername,
        email: userEmail,
        password: userPassword,
        gender: userGender as 'm' | 'f' | 'n',
        country: userCountry,
        birthDate: new Date(userBirthDate).toISOString(),
        roles: userRoles,
      };

      await userService.create({ user: newUser });

      setUserList((prev) => [...prev, newUser]);

      alert('successfully created');
      clearInputs();
    } catch (error) {
      console.error(error);
      alert((error as any)?.response?.data || (error as any).message);
    }
  };

  return (
    <>
      <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
        <div className="mb-3 d-flex w-100 justify-content-between">
          <h2>Users</h2>
          <button className="btn btn-primary" onClick={(e) => setShowFormModal(true)}>
            + New user
          </button>
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
                <th scope="col">Actions</th>
              </tr>
            </thead>
            <tbody>
              {userList.map((userItem) => (
                <UserItem key={userItem.id} userItem={userItem} />
              ))}
            </tbody>
          </table>
        </div>
      </div>
      <ReusableModal show={showFormModal}>
        <>New User</>
        <>
          <form onSubmit={handleFormSubmit}>
            <div className="mb-3">
              <InputField
                label="Username"
                id="userUsername"
                type="text"
                onChange={(e) => setUserUsername(e.target.value)}
                value={userUsername}
              />
            </div>

            <div className="mb-3">
              <InputField
                label="Email"
                id="userEmail"
                type="email"
                onChange={(e) => setUserEmail(e.target.value)}
                value={userEmail}
              />
            </div>

            <div className="mb-3">
              <InputField
                label="Password"
                id="userPassword"
                type="password"
                onChange={(e) => setUserPassword(e.target.value)}
                value={userPassword}
              />
            </div>

            <div className="mb-3">
              <InputField
                label="Birth date"
                id="userBirthDate"
                type="date"
                onChange={(e) => setUserBirthDate(e.target.value)}
                value={userBirthDate}
              />
            </div>

            <div className="mb-3">
              <label htmlFor="memberRole" className="form-label">
                Gender
              </label>
              <select
                id="userGender"
                className="form-select"
                value={userGender}
                onChange={(e) => setUserGender(e.target.value)}
              >
                <option value="label">--- User gender ---</option>
                <option value="m">Male</option>
                <option value="f">Female</option>
                <option value="n">Neutral</option>
              </select>
            </div>

            <div className="mb-3">
              <label htmlFor="memberRole" className="form-label">
                Country
              </label>
              <select
                id="userCountry"
                className="form-select"
                value={userCountry}
                onChange={(e) => setUserCountry(e.target.value)}
              >
                <option value="label">--- User country ---</option>
                {countries.map((countryItem) => (
                  <option key={countryItem.code} value={countryItem.code}>
                    {countryItem.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="mb-3">
              <label htmlFor="memberRole" className="form-label">
                Role
              </label>
              <select
                id="userRole"
                className="form-select"
                size={3}
                multiple
                onChange={(e) => setUserRoles(Array.from(e.target.selectedOptions, (option) => option.value))}
                value={userRoles}
              >
                <option value="admin">Admin</option>
                <option value="user">User</option>
                <option value="guest">Guest</option>
              </select>
              <div className="form-text">
                For windows: Hold down the control (ctrl) button to select multiple options.
              </div>
              <div className="form-text">For Mac: Hold down the command button to select multiple options</div>
            </div>

            <div className="d-flex justify-content-end" style={{ gap: '1rem' }}>
              <SecondaryBtn btnText="Close" onClick={closeNewFormModal} />
              <PrimaryBtn btnText="Save" type="submit" />
            </div>
          </form>
        </>
      </ReusableModal>
    </>
  );
};
