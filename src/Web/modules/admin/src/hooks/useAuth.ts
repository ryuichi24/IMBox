import { authService } from '@IMBoxWeb/core/dist/services';
import { useEffect, useState } from 'react';

export const useAuth = (path: string) => {
  const [loading, toggleLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    (async () => {
      try {
        const authUser = await authService.checkAuth();
        setIsAuthenticated(true);
        toggleLoading(false);
      } catch (error) {
        setIsAuthenticated(false);
        toggleLoading(false);
        setError((error as any).response?.data);
      }
    })();
  }, [path]);

  return [isAuthenticated, loading, error] as const;
};
