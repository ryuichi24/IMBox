import React from 'react';
import { Link, useLocation } from 'react-router-dom';

export const Sidebar = () => {
  const { pathname } = useLocation();

  const navItems = [
    {
      itemName: 'Dashboard',
      path: '/dashboard',
      icon: 'fa-solid fa-gauge',
    },
    {
      itemName: 'Users',
      path: '/users',
      icon: 'fa-solid fa-users',
    },
    {
      itemName: 'Movies',
      path: '/movies',
      icon: 'fa-solid fa-clapperboard',
    },
    {
      itemName: 'Members',
      path: '/members',
      icon: 'fa-solid fa-people-group',
    },
  ];
  return (
    <div className="d-flex h-100">
      <div className="h-100 d-flex flex-column flex-shrink-0 p-3 text-white bg-dark" style={{ width: '280px' }}>
        <Link
          to="/dashboard"
          className="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none"
        >
          <span className="fs-4">IMBox Admin</span>
        </Link>
        <hr />
        <ul className="nav nav-pills flex-column mb-auto">
          {navItems.map((item, index) => (
            <li className="nav-item" key={index}>
              <Link to={item.path} className={`nav-link text-white ${pathname.includes(item.path) && 'active'}`}>
                <i className={`${item.icon} pe-3`}></i>
                {item.itemName}
              </Link>
            </li>
          ))}
        </ul>
        <hr />
      </div>
    </div>
  );
};
