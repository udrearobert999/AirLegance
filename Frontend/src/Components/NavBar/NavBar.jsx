import { useState } from 'react';
import useAuth from 'Hooks/useAuth';
import useLogout from 'Hooks/useLogout';

import { Link, useNavigate } from 'react-router-dom';

import {
  AppBar,
  Box,
  Button,
  IconButton,
  Toolbar,
  useMediaQuery,
  useTheme,
  Typography,
  Drawer,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import RegisterModal from 'Components/RegisterModal';
import LoginModal from 'Components/LoginModal';
import DrawerContent from 'Components/DrawerContent';

import Style from './NavBar.module.css';

import {
  HOME_ROUTE,
  ABOUT_ROUTE,
  INFORMATION_ROUTE,
  CONTACT_ROUTE,
} from 'Routes';

const navItems = [
  { name: 'Home', route: HOME_ROUTE },
  { name: 'About', route: ABOUT_ROUTE },
  { name: 'Information', route: INFORMATION_ROUTE },
  { name: 'Contact', route: CONTACT_ROUTE },
];

const NavBar = () => {
  const [openRegisterModal, setOpenRegisterModal] = useState(false);
  const handleRegisterModalOpen = () => setOpenRegisterModal(true);
  const handleRegisterModalClose = () => setOpenRegisterModal(false);

  const [openLoginModal, setOpenLoginModal] = useState(false);
  const handleLoginModalOpen = () => setOpenLoginModal(true);
  const handleLoginModalClose = () => setOpenLoginModal(false);

  const [drawerOpen, setDrawerOpen] = useState(false);
  const handleDrawerToggle = () => setDrawerOpen(!drawerOpen);

  const { auth } = useAuth();
  const logout = useLogout();
  const navigate = useNavigate();

  const handleLogout = async () => {
    await logout();
    navigate(HOME_ROUTE);
  };

  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

  const navButtons = (
    <Box className={Style.flexDisplay}>
      {navItems.map((item) => (
        <Link key={item.name} className={Style.Link} to={item.route}>
          <Button className={Style.buttonColor}>{item.name}</Button>
        </Link>
      ))}
    </Box>
  );

  return (
    <>
      <RegisterModal
        open={openRegisterModal}
        handleClose={handleRegisterModalClose}
      />
      <LoginModal open={openLoginModal} handleClose={handleLoginModalClose} />
      <Box className={Style.grow}>
        <AppBar className={Style.appBarPosition}>
          <Toolbar className={Style.toolbar}>
            {isMobile && (
              <IconButton
                edge='start'
                color='inherit'
                aria-label='menu'
                onClick={handleDrawerToggle}
              >
                <MenuIcon />
              </IconButton>
            )}
            {!isMobile && navButtons}
            <Box className={Style.flexDisplay}>
              {auth?.user && auth.user.firstName && auth.user.lastName ? (
                <>
                  <Typography variant='h6'>
                    Welcome {auth.user.firstName} {auth.user.lastName}!
                  </Typography>
                  <Button onClick={handleLogout} className={Style.buttonColor}>
                    Logout
                  </Button>
                </>
              ) : (
                <>
                  <Button
                    onClick={handleLoginModalOpen}
                    className={Style.buttonColor}
                  >
                    Login
                  </Button>
                  <Button
                    onClick={handleRegisterModalOpen}
                    className={Style.buttonColor}
                  >
                    Register
                  </Button>
                </>
              )}
            </Box>
          </Toolbar>
        </AppBar>
        {isMobile && (
          <Drawer anchor='left' open={drawerOpen} onClose={handleDrawerToggle}>
            <DrawerContent
              items={navItems}
              handleDrawerToggle={handleDrawerToggle}
            />
          </Drawer>
        )}
      </Box>
    </>
  );
};

export default NavBar;
