import React from 'react';

export const Footer = () => {
  return (
    <footer className="d-flex flex-column align-items-center justify-content-end w-100" style={{ minHeight: '150px' }}>
      <section></section>
      <div className="text-center pb-3" style={{ backgroundColor: 'rgba(0, 0, 0, 0.05)' }}>
        © 2021 Copyright:
        <a className="text-reset fw-bold" href="https://github.com/ryuichi24/">
          ryuichi24
        </a>
      </div>
    </footer>
  );
};
