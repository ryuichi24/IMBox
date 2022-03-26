import React from 'react';
import { useAuthContext } from '@/contexts/auth-context';
import { Link } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { Spinner } from '@/components/Spinner';
import { CircleIconWrapper } from '@/components/UI/CircleIconWrapper';
import { stringToColor } from '@IMBoxWeb/core/dist/util';

export const Header = () => {
  const { pathname } = useLocation();
  const { isAuthenticated, isLoading, user, signOut } = useAuthContext();

  const navItemList = [
    {
      name: 'Home',
      path: '/',
    },
    {
      name: 'Movies',
      path: '/movies',
    },
  ];
  return (
    <header className="p-3 bg-dark text-white">
      <div className="container">
        <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
          <Link to="/" className="fw-bold d-flex align-items-center mb-2 mb-lg-0 text-white text-decoration-none">
            IMBox
          </Link>

          <ul className="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
            {navItemList.map((navItem) => (
              <li key={navItem.path}>
                <Link
                  to={navItem.path}
                  className={`nav-link px-2 ${
                    navItem.path === '/'
                      ? navItem.path === pathname
                        ? 'text-white'
                        : 'text-secondary'
                      : pathname.includes(navItem.path)
                      ? 'text-white'
                      : 'text-secondary'
                  }`}
                >
                  {navItem.name}
                </Link>
              </li>
            ))}
          </ul>

          <div className="text-end">
            {isLoading ? (
              <Spinner color="yellow" />
            ) : isAuthenticated ? (
              <div className="dropdown">
                <a
                  href="/"
                  className="d-block link-dark text-decoration-none dropdown-toggle"
                  data-bs-toggle="dropdown"
                >
                  {user && (
                    <CircleIconWrapper color={stringToColor(user.id)} size={40}>
                      {user.email[0].toUpperCase()}
                    </CircleIconWrapper>
                  )}
                </a>

                <ul className="dropdown-menu text-small dropdown-menu-dark">
                  <li>
                    <Link to={`/user-profile/${user?.id}`} className="dropdown-item">
                      Profile
                    </Link>
                  </li>
                  <li>
                    <hr className="dropdown-divider" />
                  </li>
                  <li>
                    <button className="dropdown-item" style={{ color: 'red' }} onClick={(e) => signOut()}>
                      Sign out
                    </button>
                  </li>
                </ul>
              </div>
            ) : (
              <Link className="text-decoration-none" to={`/signin`} style={{ color: 'black' }}>
                <button type="button" className="btn btn-warning">
                  Sign In
                </button>
              </Link>
            )}
            {}
          </div>
        </div>
      </div>
    </header>
  );
};
