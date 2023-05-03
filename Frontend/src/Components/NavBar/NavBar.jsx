import { useState } from 'react';

import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';
import Style from './NavBar.module.css';
import { Link } from 'react-router-dom';

import SignUpModal from 'Components/SignUpModal';

const navItems = ['Home', 'About', 'Contact'];

const getRoute = (item) => {
  if (item === 'Home') return '/';

  return '/' + item;
};

export default function NavBar() {
  const [openRegisterModal, setOpenRegisterModal] = useState(false);
  const handleRegisterModalOpen = () => setOpenRegisterModal(true);
  const handleRegisterModalClose = () => setOpenRegisterModal(false);

  return (
    <>
      <SignUpModal
        open={openRegisterModal}
        handleClose={handleRegisterModalClose}
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
              <Button sx={{ color: '#fff' }}>Login</Button>
              <Button onClick={handleRegisterModalOpen} sx={{ color: '#fff' }}>
                Register
              </Button>
            </Box>
          </Toolbar>
        </AppBar>
      </Box>
    </>
  );
}
