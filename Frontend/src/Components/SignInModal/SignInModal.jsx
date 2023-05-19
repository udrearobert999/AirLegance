import { useState } from 'react';

import {
  Avatar,
  Button,
  TextField,
  FormControlLabel,
  Checkbox,
  Grid,
  Box,
  Link,
  Typography,
  Container,
  Backdrop,
  Modal,
  Fade,
} from '@mui/material';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';

import axios from 'axios';

import Style from './SignInModal.module.css';

import useAuth from '../../Hooks/useAuth';

function Copyright(props) {
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
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

export default function SignInModal({ open, handleClose }) {
  const { setAuth } = useAuth();

  const [errors, setErrors] = useState({});

  const handleSubmit = async (event) => {
    event.preventDefault();

    const data = new FormData(event.currentTarget);
    const userLoginData = {
      email: data.get('email'),
      password: data.get('password'),
    };

    try {
      const response = await axios.post(
        'http://localhost:9200/api/auth/login',
        userLoginData,
        { withCredentials: true }
      );
      const userData = {
        user: response.data?.data,
      };
      setAuth(userData);
      handleClose();
    } catch (error) {
      if (error.response && error.response.data) {
        const errorData = error.response.data.errors.reduce((acc, cur) => {
          acc[cur.propertyName] = cur.errorMessage;
          return acc;
        }, {});

        setErrors(errorData);
      }
    }
  };

  return (
    <Modal
      aria-labelledby='transition-modal-title'
      aria-describedby='transition-modal-description'
      open={open}
      onClose={handleClose}
      closeAfterTransition
      slots={{ backdrop: Backdrop }}
      slotProps={{
        backdrop: {
          timeout: 500,
        },
      }}
    >
      <Fade in={open}>
        <Box className={Style.modalBox}>
          <Container component='main' maxWidth='xs'>
            <Box className={Style.formContainer}>
              <Avatar className={Style.avatar}>
                <LockOutlinedIcon />
              </Avatar>
              <Typography component='h1' variant='h5'>
                Sign in
              </Typography>
              <Box
                component='form'
                onSubmit={handleSubmit}
                noValidate
                className={Style.formMarginTop}
              >
                <TextField
                  margin='normal'
                  required
                  fullWidth
                  id='email'
                  label='Email Address'
                  name='email'
                  autoComplete='email'
                  autoFocus
                  error={!!errors.Email}
                  helperText={errors.Email}
                />
                <TextField
                  margin='normal'
                  required
                  fullWidth
                  name='password'
                  label='Password'
                  type='password'
                  id='password'
                  autoComplete='current-password'
                  error={!!errors.Password}
                  helperText={errors.Password}
                />
                <FormControlLabel
                  control={<Checkbox value='remember' color='primary' />}
                  label='Remember me'
                />
                <Button
                  type='submit'
                  fullWidth
                  variant='contained'
                  color='primary'
                  className={Style.submitButton}
                >
                  Sign In
                </Button>
                <Grid container>
                  <Grid item xs>
                    <Link href='#' variant='body2'>
                      Forgot password?
                    </Link>
                  </Grid>
                  <Grid item>
                    <Link href='#' variant='body2'>
                      {"Don't have an account? Sign Up"}
                    </Link>
                  </Grid>
                </Grid>
              </Box>
            </Box>
            <Copyright className={Style.copyrightMargin} />
          </Container>
        </Box>
      </Fade>
    </Modal>
  );
}
