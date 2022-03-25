import React from 'react';
import { useParams } from 'react-router-dom';

export const RatingsPage = () => {
  const { movieId } = useParams();
  return <div>RatingsPage {movieId}</div>;
};
