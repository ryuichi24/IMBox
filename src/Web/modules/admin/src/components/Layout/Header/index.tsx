import React from 'react';
import { tokenManager } from '@IMBoxWeb/core/dist/api-client/token-manager';
import { useNavigate } from 'react-router-dom';
import { CircleIconWrapper } from '@/components/UI';

export const Header = () => {
  const navigate = useNavigate();
  return (
    <header className="p-3 mb-3 border-bottom w-100">
      <div className="d-flex flex-wrap flex-row-reverse align-items-center w-100">
        <div className="dropdown text-end">
          <a href="/" className="d-block link-dark text-decoration-none" data-bs-toggle="dropdown">
            <CircleIconWrapper color="blue" size={40}>
              {'A'}
            </CircleIconWrapper>
          </a>

          <ul className="dropdown-menu text-small">
            <li>
              <a
                className="dropdown-item"
                style={{ color: 'red' }}
                href="/"
                onClick={(e) => {
                  tokenManager.accessToken.remove();
                  tokenManager.refreshToken.remove();
                  navigate('/admin/signin');
                }}
              >
                Sign out
              </a>
            </li>
          </ul>
        </div>
      </div>
    </header>
  );
};
