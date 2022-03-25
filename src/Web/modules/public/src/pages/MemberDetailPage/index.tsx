import React, { useEffect, useState } from 'react';
import { memberService } from '@IMBoxWeb/core/dist/services';
import { useParams } from 'react-router-dom';
import { MemberModel } from '@IMBoxWeb/core/src/models';
import { Link } from 'react-router-dom';
import { Heading } from '@/components/UI';

export const MemberDetailPage = () => {
  const { memberId } = useParams();
  const [memberItem, setMemberItem] = useState<MemberModel>();

  useEffect(() => {
    if (!memberId) return;

    (async () => {
      try {
        const fetchedMember = await memberService.getById({ memberId });
        setMemberItem(fetchedMember);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, [memberId]);
  return (
    <div className="shadow-sm h-auto">
      <h2 className="display-4 fw-bold mb-3">{memberItem?.name}</h2>
      <div className="d-flex" style={{ gap: '2rem' }}>
        <img
          src={memberItem?.headshotUrl}
          alt={memberItem?.name}
          className="w-auto d-block"
          style={{ maxHeight: '800px' }}
        />
        <div>
          <div>
            <Heading level={3} text="Profile" />
            <p>Name: {memberItem?.name}</p>
            <p>Role: {memberItem?.role}</p>
            <p>Birth date: {memberItem?.birthDate?.toString().split('T')[0]}</p>
          </div>

          <div>
            <Heading level={3} text="Description" />
            <p>{memberItem?.description}</p>
          </div>

          <div>
            <Heading level={3} text="Movies" />
            <div>
              <ul>
                {memberItem?.movies?.map((movieItem) => (
                  <li key={movieItem.id}>
                    <Link to={`/movies/${movieItem.id}`}>{movieItem.title}</Link>
                  </li>
                ))}
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
