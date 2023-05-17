import { useState, useEffect } from 'react';

import useAuth from '../../Hooks/useAuth';

import {
  AppBar,
  Box,
  Button,
  Drawer,
  IconButton,
  Toolbar,
  useMediaQuery,
  useTheme,
  Typography,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';

import { Link } from 'react-router-dom';

import Style from './NavBar.module.css';
import SignUpModal from 'Components/SignUpModal';
import SignInModal from 'Components/SignInModal';

const DrawerContent = ({ navItems, getRoute, handleDrawerToggle }) => {
  return (
    <Box p={2} bgcolor='background.default'>
      {navItems.map((item) => (
        <Link
          key={item}
          className={Style.Link}
          to={getRoute(item)}
          onClick={handleDrawerToggle}
        >
          <Button className={Style.drawerButtonColor} fullWidth>
            {item}
          </Button>
        </Link>
      ))}
    </Box>
  );
};

export default function NavBar() {
  const [openSignUpModal, setOpenSignUpModal] = useState(false);
  const handleSignUpModalOpen = () => setOpenSignUpModal(true);
  const handleSignUpModalClose = () => setOpenSignUpModal(false);

  const [openSignInModal, setOpenSignInModal] = useState(false);
  const handleSignInModalOpen = () => setOpenSignInModal(true);
  const handleSignInModalClose = () => setOpenSignInModal(false);

  const [drawerOpen, setDrawerOpen] = useState(false);

  const [userData, setUserData] = useState({});

  const handleLogout = () => {
    setUserData({});
  };

  const handleDrawerToggle = () => {
    setDrawerOpen(!drawerOpen);
  };

  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

  const navItems = ['Home', 'About', 'Information', 'Contact'];
  const getRoute = (item) => {
    // TODO: Modify this to be Open-Closed
    switch (item) {
      case 'Home':
        return '/';
      case 'About':
        return '/about';
      case 'Contact':
        return '/contact';
      case 'Information':
        return '/information';
      default:
        return '/';
    }
  };

  const navButtons = (
    <Box className={Style.flexDisplay}>
      {navItems.map((item) => (
        <Link key={item} className={Style.Link} to={getRoute(item)}>
          <Button className={Style.buttonColor}>{item}</Button>
        </Link>
      ))}
    </Box>
  );

  useEffect(() => {
    // At the start, check if there is a user data in local storage
    const savedUserData = JSON.parse(localStorage.getItem('userData'));
    if (savedUserData) {
      setUserData(savedUserData);
    }
  }, []);

  useEffect(() => {
    // When userData changes, save it to local storage
    if (userData && Object.keys(userData).length > 0) {
      localStorage.setItem('userData', JSON.stringify(userData));
    } else {
      localStorage.removeItem('userData');
    }
  }, [userData]);

  return (
    <>
      <SignUpModal
        open={openSignUpModal}
        handleClose={handleSignUpModalClose}
      />
      <SignInModal
        open={openSignInModal}
        handleClose={handleSignInModalClose}
        setUserData={setUserData}
      />
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
              {Object.keys(userData).length > 0 &&
              userData.firstName &&
              userData.lastName ? (
                <>
                  <Typography variant='h6'>
                    Welcome {userData.firstName} {userData.lastName}!
                  </Typography>
                  <Button onClick={handleLogout} className={Style.buttonColor}>
                    Log Out
                  </Button>
                </>
              ) : (
                <>
                  <Button
                    onClick={handleSignInModalOpen}
                    className={Style.buttonColor}
                  >
                    Login
                  </Button>
                  <Button
                    onClick={handleSignUpModalOpen}
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
              navItems={navItems}
              getRoute={getRoute}
              handleDrawerToggle={handleDrawerToggle}
            />
          </Drawer>
        )}
      </Box>
    </>
  );
}
