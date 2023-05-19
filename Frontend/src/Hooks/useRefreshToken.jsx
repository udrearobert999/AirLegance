import useAuth from './useAuth';

import axios from 'axios';

const useRefreshToken = () => {
  const { setAuth, auth } = useAuth();

  const refresh = async () => {
    const response = await axios.post(
      'http://localhost:9200/api/auth/refresh',
      null,
      {
        withCredentials: true,
      }
    );

    const userData = {
      user: response.data?.data,
    };
    setAuth(userData);
  };

  return refresh;
};

export default useRefreshToken;
