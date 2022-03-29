import React, { useEffect } from 'react';
import { Navigate, Route, Routes, useLocation } from 'react-router-dom';
// hooks
import { useAuth } from '@/hooks/useAuth';
// page components
import { UsersPage } from '@/pages/UsersPage';
import { MoviesPage } from '@/pages/MoviesPage';
import { MembersPage } from '@/pages/MembersPage';
import { SignInPage } from '@/pages/SigninPage';
// components
import { ProtectedRoute } from '@/components/ProtectedRoute';
import { Layout } from '@/components/Layout';
import { MemberDetail } from '@/containers/Members/MemberDetail';
import { MovieDetail } from '@/containers/Movies/MovieDetail';
import { UserDetail } from '@/containers/Users/UserDetail';

export const App = () => {
  const location = useLocation();
  const [isAuthenticated, loading, error] = useAuth(location.pathname);

  if (loading) return <div>loading...</div>;
  return (
    <div className="container-fluid vh-100 p-0">
      <Routes>
        <Route path="/admin/*">
          <Route element={<ProtectedRoute isAllowed={isAuthenticated} redirectPath="/admin/signin" />}>
            <Route element={<Layout />}>
              <Route path="users" element={<UsersPage />} />
              <Route path="users/:userId" element={<UserDetail />} />
              <Route path="movies" element={<MoviesPage />} />
              <Route path="movies/:movieId" element={<MovieDetail />} />
              <Route path="members" element={<MembersPage />} />
              <Route path="members/:memberId" element={<MemberDetail />} />
            </Route>
          </Route>

          <Route element={<ProtectedRoute isAllowed={!isAuthenticated} redirectPath="/admin/users" />}>
            <Route path="signin" element={<SignInPage />} />
          </Route>

          <Route path="*" element={<Navigate to="/admin/users" />} />
        </Route>

        <Route path="*" element={<Navigate to="/admin/users" />} />
      </Routes>
    </div>
  );
};
