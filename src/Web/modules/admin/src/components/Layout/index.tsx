import React from 'react';
import { Outlet } from 'react-router-dom';
import { Header } from './Header';
import { Sidebar } from './Sidebar';

export const Layout = () => {
  return (
    <div className="d-flex h-100">
      <Sidebar />
      <div className="d-flex flex-column w-100 h-100">
        <Header />
        <Outlet />
      </div>
    </div>
  );
};
