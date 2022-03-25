import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
// pages
import { HomePage } from '@/pages/HomePage';
import { MoviePage } from '@/pages/MoviePage';
import { RatingsPage } from '@/pages/RatingsPage';
import { UserProfilePage } from '@/pages/UserProfilePage';
import { SigninPage } from '@/pages/SigninPage';
// components
import { ProtectedRoute } from '@/components/ProtectedRoute';
import { Layout } from '@/components/Layout';

export const App = () => {
  const isAuthenticated = true;

  return (
    <div className="container">
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/movies/:movieId" element={<MoviePage />} />
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
    </div>
  );
};
