import React, { useState } from 'react';
import { UserModel } from '@IMBoxWeb/core/dist/models';
import { countries } from '@IMBoxWeb/core/dist/util';
import { userService } from '@IMBoxWeb/core/dist/services';
import { Spinner } from '@/components/Spinner';

interface Props {
  userItem?: UserModel;
  handleUserProfileUpdate: (updatedUser: UserModel) => void;
}

export const ProfileEditForm = ({ userItem, handleUserProfileUpdate }: Props) => {
  const [userUsername, setUserUsername] = useState(userItem?.username);
  const [userGender, setUserGender] = useState<'m' | 'f' | 'n'>(userItem?.gender || 'n');
  const [userCountry, setUserCountry] = useState(userItem?.country);
  const [userBirthDate, setBirthDate] = useState(userItem?.birthDate?.toString()?.split('T')[0] || '');
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    e.preventDefault();
    if (!userItem) return;

    setIsLoading(true);
    const userToUpdate = {
      username: userUsername,
      gender: userGender,
      country: userCountry,
      birthDate: userBirthDate,
    };

    await userService.update({ user: userToUpdate, userId: userItem.id! });
    handleUserProfileUpdate({ ...userItem, ...userToUpdate });
    setIsLoading(false);

    alert('successfully updated');
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="userUsername" className="form-label">
          Username
        </label>
        <input
          className={`form-control`}
          placeholder="username"
          id="userUsername"
          type="text"
          value={userUsername}
          onChange={(e) => setUserUsername(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="memberRole" className="form-label">
          Gender
        </label>
        <select
          id="userGender"
          className="form-select"
          value={userGender}
          onChange={(e) => setUserGender(e.target.value as 'm' | 'f' | 'n')}
        >
          <option value="label">--- User gender ---</option>
          <option value="m">Male</option>
          <option value="f">Female</option>
          <option value="n">Neutral</option>
        </select>
      </div>
      <div>
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
      <div>
        <label htmlFor="userUsername" className="form-label">
          Birth date
        </label>
        <input
          className={`form-control`}
          placeholder="username"
          id="userUsername"
          type="date"
          value={userBirthDate}
          onChange={(e) => setBirthDate(e.target.value)}
        />
      </div>
      <div className="d-flex justify-content-end mt-5">
        {isLoading ? <Spinner color="yellow" /> : <button className="btn btn-warning">Save</button>}
      </div>
    </form>
  );
};
