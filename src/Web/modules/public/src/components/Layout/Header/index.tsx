import React from 'react';
import { useAuthContext } from '@/contexts/auth-context';
import { Link } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { Spinner } from '@/components/Spinner';

export const Header = () => {
  const { pathname } = useLocation();
  const { isAuthenticated, isLoading } = useAuthContext();

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
            ) : (
              !isAuthenticated && (
                <button type="button" className="btn btn-warning">
                  <Link className="text-decoration-none" to={`/signin`} style={{ color: 'black' }}>
                    Sign In
                  </Link>
                </button>
              )
            )}
            {}
          </div>
        </div>
      </div>
    </header>
  );
};
