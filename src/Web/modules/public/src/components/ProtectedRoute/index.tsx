import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { Spinner } from '../Spinner';

interface Props {
  isAllowed: boolean;
  isAuthenticating: boolean;
  redirectPath?: string;
  children?: React.ReactNode;
}

export const ProtectedRoute = ({
  isAllowed,
  redirectPath = '/signin',
  isAuthenticating = true,
  children,
}: Props): JSX.Element => {
  if (isAuthenticating) {
    return <Spinner color="yellow" />;
  }

  if (!isAllowed) {
    return <Navigate to={redirectPath} replace />;
  }

  return <>{children ? children : <Outlet />}</>;
};
