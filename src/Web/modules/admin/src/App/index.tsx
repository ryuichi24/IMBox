import React, { useEffect } from 'react';
import { Navigate, Route, Routes, useLocation } from 'react-router-dom';
// hooks
import { useAuth } from '@/hooks/useAuth';
// page components
import { Dashboard } from '@/pages/Dashboard';
import { SignInPage } from '@/pages/SigninPage';
// components
import { ProtectedRoute } from '@/components/ProtectedRoute';
import { Sidebar } from '@/components/Layout/Sidebar';
import { UsersPage } from '@/pages/UsersPage';
import { MoviesPaage } from '@/pages/MoviesPage';
import { MembersPage } from '@/pages/MembersPage';

export const App = () => {
  const location = useLocation();
  const [isAuthenticated, loading, error] = useAuth(location.pathname);

  if (loading) return <div>loading...</div>;
  return (
    <div className="container-fluid vh-100 p-0">
      <Routes>
        <Route element={<ProtectedRoute isAllowed={isAuthenticated} redirectPath="/signin" />}>
          <Route path="/" element={<Sidebar />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/users" element={<UsersPage />} />
            <Route path="/movies" element={<MoviesPaage />} />
            <Route path="/members" element={<MembersPage />} />
          </Route>
        </Route>

        <Route element={<ProtectedRoute isAllowed={!isAuthenticated} redirectPath="/" />}>
          <Route path="/signin" element={<SignInPage />} />
        </Route>

        <Route path="*" element={<Navigate to="/dashboard" />} />
      </Routes>
    </div>
  );
};
