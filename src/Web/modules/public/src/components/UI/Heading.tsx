import React from 'react';

interface Props {
  text: string;
  level?: number;
}

export const Heading = ({ text, level = 1 }: Props) => {
  const HeadingTag = `h${level}` as keyof JSX.IntrinsicElements;
  return (
    <>
      <HeadingTag className="border-start border-warning border-4 ps-2 mb-3">{text}</HeadingTag>
      <hr />
    </>
  );
};
