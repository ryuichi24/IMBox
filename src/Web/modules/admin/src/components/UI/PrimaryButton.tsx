import React from 'react';

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  btnText: string;
  className?: string;
}

export const PrimaryBtn = ({ btnText, className, ...rest }: Props) => {
  return (
    <button className={`btn btn-primary ${className}`} {...rest}>
      {btnText}
    </button>
  );
};
