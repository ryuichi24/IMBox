import React, { createContext, ReactNode, useContext, useEffect, useState } from 'react';
import { authService } from '@IMBoxWeb/core/dist/services';
import { UserModel } from '@IMBoxWeb/core/dist/models';

interface AuthState {
  user?: UserModel;
  isAuthenticated: boolean;
  setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>> | null;
  isLoading: boolean;
}

const AuthContext = createContext<AuthState>({
  user: {},
  isAuthenticated: false,
  setIsAuthenticated: null,
  isLoading: true,
});

interface Props {
  children: ReactNode;
}

export const AuthContextProvider = ({ children }: Props): JSX.Element => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<UserModel>({});
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    (async () => {
      try {
        const authUser = await authService.checkAuth();
        setUser(authUser);
        setIsAuthenticated(true);
        setIsLoading(false);
      } catch (error) {
        setIsAuthenticated(false);
        setIsLoading(false);
      }
    })();
  }, []);

  return (
    <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated, isLoading, user }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuthContext = (): AuthState => useContext(AuthContext);
