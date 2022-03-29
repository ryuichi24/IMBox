import React from 'react';

export const Header = () => {
  return (
    <header className="p-3 mb-3 border-bottom w-100">
      <div className="d-flex flex-wrap flex-row-reverse align-items-center w-100">
        <div className="dropdown text-end">
          <a href="/" className="d-block link-dark text-decoration-none dropdown-toggle" data-bs-toggle="dropdown">
            <img
              src="https://images.unsplash.com/photo-1529778873920-4da4926a72c2?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1472&q=80"
              alt="cat"
              width="32"
              height="32"
              className="rounded-circle"
            />
          </a>

          <ul className="dropdown-menu text-small">
            <li>
              <a className="dropdown-item" style={{ color: 'red' }} href="/">
                Sign out
              </a>
            </li>
          </ul>
        </div>
      </div>
    </header>
  );
};
