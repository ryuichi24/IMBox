import React, { createContext, ReactNode, useContext, useEffect, useState } from 'react';
import { authService } from '@IMBoxWeb/core/dist/services';
import { UserModel } from '@IMBoxWeb/core/dist/models';
import { tokenManager } from '@IMBoxWeb/core/dist/api-client/token-manager';
import { useNavigate } from 'react-router-dom';

interface SignInPayload {
  email: string;
  password: string;
}

interface AuthState {
  user: UserModel | null;
  isAuthenticated: boolean;
  setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
  setUser: React.Dispatch<React.SetStateAction<UserModel | null>>;
  isLoading: boolean;
  signIn: (payload: SignInPayload) => Promise<any>;
  signOut: () => void;
}

const AuthContext = createContext<AuthState>({
  user: null,
  isAuthenticated: false,
  setIsAuthenticated: () => null,
  setUser: () => null,
  isLoading: true,
  signIn: (payload: SignInPayload) => new Promise((rs, _) => rs(null)),
  signOut: () => null,
});

interface Props {
  children: ReactNode;
}

export const AuthContextProvider = ({ children }: Props): JSX.Element => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<UserModel | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    (async () => {
      try {
        const authUser = await authService.checkAuth();
        setIsAuthenticated(true);
        setUser(authUser);
        setIsLoading(false);
      } catch (error) {
        setIsAuthenticated(false);
        setIsLoading(false);
      }
    })();
  }, []);

  const signOut = () => {
    setUser(null);
    setIsAuthenticated(false);
    tokenManager.accessToken.remove();
    tokenManager.refreshToken.remove();
    navigate('/');
  };

  const signIn = async ({ email, password }: SignInPayload) => {
    try {
      setIsLoading(true);
      await authService.signIn({ email, password });
      const authUser = await authService.checkAuth();
      setIsLoading(false);
      setUser(authUser);
      setIsAuthenticated(true);
    } catch (error) {
      const errorMsg = (error as any).response.data;
      alert(errorMsg);
    }
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated, isLoading, user, signOut, setUser, signIn }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuthContext = (): AuthState => useContext(AuthContext);
