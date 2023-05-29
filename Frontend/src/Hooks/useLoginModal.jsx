import { useContext } from 'react';

import { LoginModalContext } from 'Contexts/LoginModalProvider';

const useLoginModal = () => {
  return useContext(LoginModalContext);
};

export default useLoginModal;
