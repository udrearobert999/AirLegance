import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useAuth from 'Hooks/useAuth';
import usePrevious from 'Hooks/usePrevious';

import { Outlet } from 'react-router-dom';

import { HOME_ROUTE } from 'Routes';

import useLoginModal from 'Hooks/useLoginModal';

function RequireAuth({ allowedRoles }) {
  const { auth } = useAuth();
  const { handleLoginModalOpen, handleLoginModalClose, openLoginModal } =
    useLoginModal();
  const navigate = useNavigate();

  const prevOpenLoginModal = usePrevious(openLoginModal);

  useEffect(() => {
    if (!auth?.user && !openLoginModal) {
      handleLoginModalOpen();
    }
  }, [auth, openLoginModal, handleLoginModalOpen]);

  useEffect(() => {
    if (prevOpenLoginModal && !openLoginModal && !auth?.user) {
      navigate(HOME_ROUTE);
      handleLoginModalClose();
    }
  }, [prevOpenLoginModal, openLoginModal, auth, navigate]);

  if (!auth?.user) {
    return null;
  }

  if (allowedRoles && !allowedRoles.includes(auth.user.role)) {
    navigate(HOME_ROUTE);
    handleLoginModalClose();
  }

  return <Outlet />;
}

export default RequireAuth;
