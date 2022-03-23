import { authService } from '@/services/auth-service';
import React, { useEffect } from 'react';

export const App = () => {
  useEffect(() => {
    console.log('mounted!');
    authService.signIn({ email: 'test@example.com', password: '11111111' }).then((result) => console.log(result));
  });
  return <div>IMBox Admin</div>;
};
