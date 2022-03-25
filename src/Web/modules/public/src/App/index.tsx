import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
// pages
import { HomePage } from '@/pages/HomePage';
import { MovieDetailPage } from '../pages/MovieDetailPage';
import { RatingsPage } from '@/pages/RatingsPage';
import { UserProfilePage } from '@/pages/UserProfilePage';
import { SigninPage } from '@/pages/SigninPage';
// components
import { ProtectedRoute } from '@/components/ProtectedRoute';
import { Layout } from '@/components/Layout';
import { MoviesPage } from '@/pages/MoviesPage';
import { MemberDetailPage } from '@/pages/MemberDetailPage';
// contexts
import { useAuthContext } from '@/contexts/auth-context';

export const App = () => {
  const { isAuthenticated } = useAuthContext();

  return (
    <>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/movies" element={<MoviesPage />} />
          <Route path="/movies/:movieId" element={<MovieDetailPage />} />
          <Route path="/members/:memberId" element={<MemberDetailPage />} />
          <Route path="/movies/:movieId/ratings" element={<RatingsPage />} />

          <Route element={<ProtectedRoute isAllowed={isAuthenticated} redirectPath="/signin" />}>
            <Route path="/user-profile/:userId" element={<UserProfilePage />} />
          </Route>

          <Route element={<ProtectedRoute isAllowed={!isAuthenticated} redirectPath="/" />}>
            <Route path="/signin" element={<SigninPage />} />
          </Route>
        </Route>

        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </>
  );
};
