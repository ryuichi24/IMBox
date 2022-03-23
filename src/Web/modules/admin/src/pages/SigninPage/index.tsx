import { authService } from '@/services/auth-service';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const SignInPage = () => {
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const clearInputs = () => {
    setEmail('');
    setPassword('');
  };

  const handleSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    e.preventDefault();

    try {
      await authService.signIn({ email, password });
      clearInputs();
      navigate('/');
    } catch (error) {
      const errorMsg = (error as any).response.data;
      alert(errorMsg);
    }
  };

  return (
    <div
      className="w-100 h-100"
      style={{
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }}
    >
      <main className="w-100 m-auto p-1" style={{ maxWidth: '330px' }}>
        <h1 className="h3 mb-3 fw-normal">Sign in</h1>
        <form className="d-flex flex-column" style={{ gap: '1rem' }} onSubmit={handleSubmit}>
          <div className="form-floating">
            <input
              type="email"
              className="form-control"
              id="floatingInput"
              placeholder="name@example.com"
              onChange={(e) => setEmail(e.target.value)}
              value={email}
            />
            <label htmlFor="floatingInput">Email address</label>
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
            <label htmlFor="floatingPassword">Password</label>
          </div>

          <button className="w-100 btn btn-lg btn-primary" type="submit">
            Sign in
          </button>
        </form>
      </main>
    </div>
  );
};
