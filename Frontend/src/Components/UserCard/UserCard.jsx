import { useState } from 'react';

import { useNavigate } from 'react-router-dom';
import useLogout from 'Hooks/useLogout';

import { HOME_ROUTE, PROFILE_ROUTE } from 'Routes';

import {
  Card,
  CardContent,
  Typography,
  Menu,
  MenuItem,
  ListItemIcon,
  Box,
} from '@mui/material';

import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';

const UserCard = ({ name }) => {
  const [anchorEl, setAnchorEl] = useState(null);

  const logout = useLogout();
  const navigate = useNavigate();

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleSignOutClicked = async () => {
    handleClose();
    await logout();
    navigate(HOME_ROUTE);
  };

  const handleProfileClicked = async () => {
    handleClose();
    navigate(PROFILE_ROUTE);
  };

  return (
    <>
      <Card
        id='userCard'
        onClick={handleClick}
        variant='outlined'
        style={{ borderRadius: 40, padding: 0 }}
      >
        <CardContent style={{ padding: 0 }}>
          <Box
            display='flex'
            alignItems='center'
            justifyContent='center'
            style={{ height: '100%', padding: '16px' }}
          >
            <Typography variant='body1' component='' style={{ marginRight: 8 }}>
              {name}
            </Typography>
            <ArrowDropDownIcon />
          </Box>
        </CardContent>
      </Card>
      <Menu
        id='simple-menu'
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={handleClose}
      >
        <MenuItem onClick={handleProfileClicked}>
          <ListItemIcon>
            <AccountCircleIcon fontSize='small' />
          </ListItemIcon>
          Profile
        </MenuItem>
        <MenuItem onClick={handleSignOutClicked}>
          <ListItemIcon>
            <ExitToAppIcon fontSize='small' />
          </ListItemIcon>
          Sign Out
        </MenuItem>
      </Menu>
    </>
  );
};

export default UserCard;
