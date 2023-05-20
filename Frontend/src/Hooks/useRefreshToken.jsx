import useAuth from './useAuth';

import axios from 'Api/Axios';

const useRefreshToken = () => {
  const { setAuth, auth } = useAuth();

  const refresh = async () => {
    const response = await axios.post('/auth/refresh', null, {
      withCredentials: true,
    });

    setAuth(response.data?.data);
  };

  return refresh;
};

export default useRefreshToken;
