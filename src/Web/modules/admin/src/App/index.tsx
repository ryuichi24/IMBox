import React, { useEffect } from 'react';
import { Navigate, Route, Routes, useLocation } from 'react-router-dom';
// hooks
import { useAuth } from '@/hooks/useAuth';
// page components
import { Dashboard } from '@/pages/Dashboard';
import { UsersPage } from '@/pages/UsersPage';
import { MoviesPaage } from '@/pages/MoviesPage';
import { MembersPage } from '@/pages/MembersPage';
import { SignInPage } from '@/pages/SigninPage';
// components
import { ProtectedRoute } from '@/components/ProtectedRoute';
import { Layout } from '@/components/Layout';
import { MemberDetail } from '@/containers/Members/MemberDetail';

export const App = () => {
  const location = useLocation();
  const [isAuthenticated, loading, error] = useAuth(location.pathname);

  if (loading) return <div>loading...</div>;
  return (
    <div className="container-fluid vh-100 p-0">
      <Routes>
        <Route element={<ProtectedRoute isAllowed={isAuthenticated} redirectPath="/signin" />}>
          <Route path="/" element={<Layout />}>
            <Route path="/" element={<Navigate to="/dashboard" />} />
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/users" element={<UsersPage />} />
            <Route path="/movies" element={<MoviesPaage />} />
            <Route path="/members" element={<MembersPage />} />
            <Route path="/members/:memberId" element={<MemberDetail />} />
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
