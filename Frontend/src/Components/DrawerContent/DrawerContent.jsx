import { Box, Button } from '@mui/material';
import { Link } from 'react-router-dom';

import Style from './DrawerContent.module.css';

const DrawerContent = ({ handleDrawerToggle, items }) => {
  return (
    <Box p={2} bgcolor='background.default'>
      {items.map((item) => (
        <Link
          key={item.name}
          className={Style.Link}
          to={item.route}
          onClick={handleDrawerToggle}
        >
          <Button className={Style.drawerButtonColor} fullWidth>
            {item.name}
          </Button>
        </Link>
      ))}
    </Box>
  );
};

export default DrawerContent;
