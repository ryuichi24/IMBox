import React from 'react';

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  btnText: string;
  className?: string;
}

export const SecondaryBtn = ({ btnText, className, ...rest }: Props) => {
  return (
    <button className={`btn btn-secondary ${className}`} {...rest}>
      {btnText}
    </button>
  );
};
