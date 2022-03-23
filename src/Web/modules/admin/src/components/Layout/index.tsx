import React from 'react';
import { Outlet } from 'react-router-dom';
import { Sidebar } from './Sidebar';

export const Layout = () => {
  return (
    <div className="d-flex h-100">
      <Sidebar />
      <div className="d-flex flex-column">
        <div>Header</div>
        <Outlet />
      </div>
    </div>
  );
};
