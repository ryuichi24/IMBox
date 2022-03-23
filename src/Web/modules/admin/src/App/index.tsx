import React, { useEffect } from 'react';
import { Navigate, Route, Routes, useLocation } from 'react-router-dom';
// hooks
import { useAuth } from '@/hooks/useAuth';
// page components
import { SignInPage } from '@/pages/SigninPage';
// components
import { ProtectedRoute } from '@/components/ProtectedRoute';

export const App = () => {
  const location = useLocation();
  const [isAuthenticated, loading, error] = useAuth(location.pathname);

  if (loading) return <div>loading...</div>;
  return (
    <div className="container-fluid vh-100 p-0">
      <Routes>
        <Route element={<ProtectedRoute isAllowed={isAuthenticated} redirectPath="/signin" />}>
          <Route path="/*" element={<div>Home</div>} />
        </Route>

        <Route element={<ProtectedRoute isAllowed={!isAuthenticated} redirectPath="/" />}>
          <Route path="/signin" element={<SignInPage />} />
        </Route>

        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </div>
  );
};
