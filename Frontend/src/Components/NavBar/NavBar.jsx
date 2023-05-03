import { useState } from 'react';

import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';
import Style from './NavBar.module.css';
import { Link } from 'react-router-dom';

import SignUpModal from 'Components/SignUpModal';
import SignInModal from 'Components/SignInModal';

const navItems = ['Home', 'About', 'Contact'];

const getRoute = (item) => {
  if (item === 'Home') return '/';

  return '/' + item;
};

export default function NavBar() {
  const [openSignUpModal, setOpenSignUpModal] = useState(false);
  const handleSignUpModalOpen = () => setOpenSignUpModal(true);
  const handleSignUpModalClose = () => setOpenSignUpModal(false);

  const [openSignInModal, setOpenSignInModal] = useState(false);
  const handleSignInModalOpen = () => setOpenSignInModal(true);
  const handleSignInModalClose = () => setOpenSignInModal(false);

  return (
    <>
      <SignUpModal
        open={openSignUpModal}
        handleClose={handleSignUpModalClose}
      />
      <SignInModal
        open={openSignInModal}
        handleClose={handleSignInModalClose}
      />
      <Box sx={{ flexGrow: 1 }}>
        <AppBar position='static'>
          <Toolbar sx={{ justifyContent: 'space-between' }}>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              {navItems.map((item) => (
                <Link key={item} className={Style.Link} to={getRoute(item)}>
                  <Box sx={{ display: { xs: 'none', sm: 'block' } }}>
                    <Button sx={{ color: '#fff' }}>{item}</Button>
                  </Box>
                </Link>
              ))}
            </Box>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              <Button onClick={handleSignInModalOpen} sx={{ color: '#fff' }}>
                Login
              </Button>
              <Button onClick={handleSignUpModalOpen} sx={{ color: '#fff' }}>
                Register
              </Button>
            </Box>
          </Toolbar>
        </AppBar>
      </Box>
    </>
  );
}
