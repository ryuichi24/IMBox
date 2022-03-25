import React from 'react';

interface Props {
  color: 'blue' | 'grey' | 'green' | 'red' | 'yellow' | 'aqua' | 'white' | 'black';
}

export const Spinner = ({ color }: Props) => {
  let colorOption;

  if (color === 'blue') colorOption = 'primary';
  if (color === 'grey') colorOption = 'secondary';
  if (color === 'green') colorOption = 'success';
  if (color === 'red') colorOption = 'danger';
  if (color === 'yellow') colorOption = 'warning';
  if (color === 'aqua') colorOption = 'info';
  if (color === 'white') colorOption = 'light';
  if (color === 'black') colorOption = 'dark';

  return <div className={`spinner-border text-${colorOption}`}></div>;
};
