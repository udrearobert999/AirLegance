import axios from 'Api/Axios';

import useAuth from './useAuth';

const useLogout = () => {
  const { setAuth } = useAuth();

  const logout = async () => {
    setAuth(null);
    try {
      const response = await axios.delete('/auth/logout', {
        withCredentials: true,
      });
    } catch (err) {
      console.log(err);
    }
  };

  return logout;
};

export default useLogout;
