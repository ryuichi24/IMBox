import React, { useEffect } from 'react';
import { authService } from '@/services/auth-service';
import { userService } from '@/services/user-service';
import { memberService } from '@/services/member-service';

export const App = () => {
  useEffect(() => {
    console.log('mounted!');
    authService
      .signIn({ email: 'test@example.com', password: '11111111' })
      .then((result) => console.log(result))
      .then(() => {
        userService.get({ page: 1 }).then((result) => {
          console.log(result);

          userService.getById({ userId: result[0].id }).then((result) => console.log(result));

          memberService.get({ page: 1 }).then((result) => {
            console.log(result);
          });
        });
      });
  });
  return <div>IMBox Admin</div>;
};
