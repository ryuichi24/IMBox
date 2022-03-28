import React, { useState } from 'react';
import { Spinner } from '@/components/Spinner';
import { authService } from '@IMBoxWeb/core/dist/services';
import { useNavigate } from 'react-router-dom';

export const SignupPage = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const clearInputs = () => {
    setUsername('');
    setEmail('');
    setPassword('');
  };

  const handleSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    e.preventDefault();
    clearInputs();

    try {
      setIsLoading(true);
      await authService.signUp({ username, email, password });
      setIsLoading(false);
      alert('Email has been sent to your mail box. Please check it to confirm your email address.');
      navigate('/');
    } catch (error) {
      console.error(error);
      alert((error as any)?.response?.data || (error as any).message);
    }
  };

  return (
    <div
      className="w-100 h-100 mt-5"
      style={{
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }}
    >
      <main className="w-100 m-auto p-1" style={{ maxWidth: '330px' }}>
        <h1 className="h3 mb-3 fw-normal">Sign up</h1>
        <form className="d-flex flex-column" style={{ gap: '1rem' }} onSubmit={handleSubmit}>
          <div className="form-floating">
            <input
              type="text"
              className="form-control"
              id="floatingUsernameInput"
              placeholder="username"
              onChange={(e) => setUsername(e.target.value)}
              value={username}
            />
            <label style={{ color: '#777777' }} htmlFor="floatingUsernameInput">
              Username
            </label>
          </div>

          <div className="form-floating">
            <input
              type="email"
              className="form-control"
              id="floatingEmailInput"
              placeholder="name@example.com"
              onChange={(e) => setEmail(e.target.value)}
              value={email}
            />
            <label style={{ color: '#777777' }} htmlFor="floatingEmailInput">
              Email address
            </label>
          </div>

          <div className="form-floating">
            <input
              type="password"
              className="form-control"
              id="floatingPassword"
              placeholder="Password"
              onChange={(e) => setPassword(e.target.value)}
              value={password}
            />
            <label style={{ color: '#777777' }} htmlFor="floatingPassword">
              Password
            </label>
          </div>

          <button className="w-100 btn btn-lg btn-warning" type="submit">
            {isLoading ? <Spinner color="black" /> : <span> Sign up </span>}
          </button>
        </form>
      </main>
    </div>
  );
};
