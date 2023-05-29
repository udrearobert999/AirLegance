import Style from './Copyright.module.css';

import { Typography } from '@mui/material';
import { Link } from 'react-router-dom';

const Copyright = (props) => {
  return (
    <Typography
      className={Style.copyrightTypography}
      variant='body2'
      color='text.secondary'
      align='center'
      {...props}
    >
      {'Copyright © '}
      <Link color='inherit' href='https://mui.com/'>
        Airlegance
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
};

export default Copyright;
