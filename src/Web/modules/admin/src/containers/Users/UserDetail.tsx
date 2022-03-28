import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { UserModel } from '@IMBoxWeb/core/dist/models';
import { userService } from '@IMBoxWeb/core/dist/services';
import { countries } from '@IMBoxWeb/core/dist/util/countries';
import { PrimaryBtn, SecondaryBtn } from '@/components/UI';
import { InputField } from '@/components/InputField';

export const UserDetail = () => {
  const { userId } = useParams();
  const navigate = useNavigate();

  const [userUsername, setUserUsername] = useState('');
  const [userEmail, setUserEmail] = useState('');
  const [userPassword, setUserPassword] = useState('');
  const [userBirthDate, setUserBirthDate] = useState('');
  const [userGender, setUserGender] = useState('');
  const [userCountry, setUserCountry] = useState('');
  const [userRoles, setUserRoles] = useState<string[]>([]);

  const setInputs = (fetchedUser: UserModel) => {
    setUserUsername(fetchedUser.username || '');
    setUserEmail(fetchedUser.email || '');
    setUserPassword(fetchedUser.password || '');
    setUserBirthDate(fetchedUser?.birthDate?.toString()?.split('T')[0] || '');
    setUserGender(fetchedUser.gender || '');
    setUserCountry(fetchedUser.country || '');
    setUserRoles(fetchedUser.roles || []);
  };

  useEffect(() => {
    if (!userId) return;

    (async () => {
      try {
        const fetchedUser = await userService.getById({ userId });
        setInputs(fetchedUser);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, [userId]);

  const handleFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    try {
      if (!userId) return;
      e.preventDefault();

      const userToUpdate: UserModel = {
        username: userUsername === '' ? undefined : userUsername,
        email: userEmail === '' ? undefined : userEmail,
        password: userPassword === '' ? undefined : userPassword,
        gender: userGender === '' ? undefined : (userGender as 'm' | 'f' | 'n'),
        country: userCountry === '' ? undefined : userCountry,
        birthDate: userBirthDate === '' ? undefined : new Date(userBirthDate).toISOString(),
        roles: userRoles,
      };

      await userService.update({ user: userToUpdate, userId });

      alert('successfully updated');
    } catch (error) {
      console.error(error);
      alert((error as any)?.response?.data || (error as any).message);
    }
  };

  const handleDelete = async () => {
    if (!userId) return;
    const confirmed = confirm('Are you sure you want to delete it?');
    if (!confirmed) return;
    try {
      await userService.remove({ userId });
      alert('Successfully removed');
      navigate('/users');
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
      <div className="card h-100 p-4">
        <div className="d-flex justify-content-end">
          <button className="btn btn-danger" style={{ width: '80px' }} onClick={handleDelete}>
            Delete
          </button>
        </div>

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
              formText="Password does not appear, but you can type new one above"
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
            <SecondaryBtn btnText="Close" onClick={() => navigate('/users')} />
            <PrimaryBtn btnText="Save" type="submit" />
          </div>
        </form>
      </div>
    </div>
  );
};
