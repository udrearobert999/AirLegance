import { Outlet } from 'react-router';

import { useState, useEffect } from 'react';

import useRefreshToken from 'Hooks/useRefreshToken';
import useAuth from 'Hooks/useAuth';

const PersistLogin = () => {
  const [isLoading, setIsLoading] = useState(true);
  const refresh = useRefreshToken();
  const { auth } = useAuth();

  useEffect(() => {
    const verifyRefresh = async () => {
      try {
        await refresh();
      } catch (err) {
      } finally {
        setIsLoading(false);
      }
    };

    !auth?.user ? verifyRefresh() : setIsLoading(false);
  }, []);

  useEffect(() => {
    console.log(`isLoading: ${isLoading}`);
    console.log(`user : ${JSON.stringify(auth?.user)}`);
  }, [isLoading]);

  return <>{isLoading ? <p>Loading...</p> : <Outlet />}</>;
};

export default PersistLogin;
