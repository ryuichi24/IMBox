import React from 'react';
import { Outlet } from 'react-router-dom';
import { Header } from './Header';

export const Layout = () => {
  return (
    <>
      <Header />
      <div className="container pt-3">
        <Outlet />
      </div>
    </>
  );
};
