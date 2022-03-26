import React from 'react';

interface Props {
  children: React.ReactNode;
  size?: number | string;
  color?: string;
}

export const CircleIconWrapper = ({ children, size = 50, color = '#938005' }: Props) => {
  return (
    <>
      <div
        style={{
          background: color,
          borderRadius: '50%',
          color: 'white',
          display: 'inline-block',
          height: `${size}px`,
          width: `${size}px`,
          margin: '0 auto',
        }}
      >
        <span
          style={{
            display: 'table-cell',
            verticalAlign: 'middle',
            height: `${size}px`,
            width: `${size}px`,
            textAlign: 'center',
            padding: '5px',
          }}
        >
          {children}
        </span>
      </div>
    </>
  );
};
