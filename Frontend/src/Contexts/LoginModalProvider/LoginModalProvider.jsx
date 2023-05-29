import {
  createContext,
  useState,
  useCallback,
  useContext,
  useEffect,
} from 'react';

export const LoginModalContext = createContext();

export const LoginModalProvider = ({ children }) => {
  const [openLoginModal, setOpenLoginModal] = useState(false);
  const handleLoginModalOpen = useCallback(() => setOpenLoginModal(true), []);
  const handleLoginModalClose = useCallback(() => setOpenLoginModal(false), []);

  return (
    <LoginModalContext.Provider
      value={{
        openLoginModal,
        handleLoginModalOpen,
        handleLoginModalClose,
      }}
    >
      {children}
    </LoginModalContext.Provider>
  );
};
