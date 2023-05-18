import useAuth from './useAuth';

import axios from 'axios';

const useRefreshToken = () => {
  const { setAuth } = useAuth();

  const refresh = async () => {
    const response = await axios.post(
      'http://localhost:9200/api/auth/refresh',
      null,
      {
        withCredentials: true,
      }
    );

    setAuth((prev) => ({
      ...prev, // user: in case we would want to change the user data in the future,
    }));
  };

  return refresh;
};

export default useRefreshToken;
