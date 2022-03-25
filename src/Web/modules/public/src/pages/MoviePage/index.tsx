import React from 'react';
import { useParams } from 'react-router-dom';

export const MoviePage = () => {
  const { movieId } = useParams();
  return <div>MoviePage {movieId}</div>;
};
