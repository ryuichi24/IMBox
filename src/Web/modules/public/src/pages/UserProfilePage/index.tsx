import React, { useEffect, useState } from 'react';
import { UserModel } from '@IMBoxWeb/core/dist/models';
import { userService } from '@IMBoxWeb/core/dist/services';
import { useParams } from 'react-router-dom';
import { CircleIconWrapper } from '@/components/UI/CircleIconWrapper';
import { useAuthContext } from '@/contexts/auth-context';
import { Spinner } from '@/components/Spinner';
import { countries } from '@IMBoxWeb/core/dist/util';
import { ProfileEditForm } from '@/containers/UserProfile/ProfileEditForm';

export const UserProfilePage = () => {
  const { userId } = useParams();
  const [userItem, setUserItem] = useState<UserModel>();
  const [isLoading, setIsLoading] = useState(true);
  const [isEditing, setIsEditing] = useState(false);
  const { user } = useAuthContext();

  useEffect(() => {
    if (!userId) return;

    (async () => {
      setIsLoading(true);
      const fetchedUser = await userService.getById({ userId });
      setUserItem(fetchedUser);
      setIsLoading(false);
      console.log(fetchedUser);
    })();
  }, [userId]);

  const handleUserProfileUpdate = (updatedUser: UserModel) => {
    setUserItem(updatedUser);
  };
  return (
    <>
      <div className="card bg-dark" style={{ width: '100%' }}>
        {isLoading ? (
          <Spinner color="yellow" />
        ) : (
          <div className="d-flex" style={{ gap: '0.5rem', minHeight: '180px' }}>
            <div
              className="d-flex justify-content-center align-items-center"
              style={{ flex: '0.4 0 100px', minWidth: '0' }}
            >
              {userItem?.username && (
                <CircleIconWrapper size={120}>
                  <span style={{ fontSize: '30px' }}>{userItem.username[0].toLocaleUpperCase()}</span>
                </CircleIconWrapper>
              )}
            </div>
            <div className="d-flex justify-content-between p-4" style={{ flex: '1 1 auto', minWidth: '0' }}>
              {user?.id === userId && isEditing ? (
                <ProfileEditForm userItem={userItem} handleUserProfileUpdate={handleUserProfileUpdate} />
              ) : (
                <div>
                  <h3 style={{ marginBottom: '0' }}>{userItem?.username}</h3>
                  <small style={{ color: '#afafaf' }}>Joined on {userItem?.createdAt.split('T')[0]}</small>
                  {user?.id === userId && (
                    <>
                      <div>Gender: {userItem?.gender}</div>
                      <div>
                        Country: {countries.find((countryItem) => countryItem.code === userItem?.country)?.name}
                      </div>
                      <div>Birth date: {userItem?.birthDate?.split('T')[0]}</div>
                    </>
                  )}
                </div>
              )}
              {user?.id === userId && (
                <div>
                  {isEditing ? (
                    <button className="btn btn-warning" onClick={(e) => setIsEditing(false)}>
                      Cancel
                    </button>
                  ) : (
                    <button className="btn btn-warning" onClick={(e) => setIsEditing(true)}>
                      Edit
                    </button>
                  )}
                </div>
              )}
            </div>
          </div>
        )}
      </div>
    </>
  );
};
