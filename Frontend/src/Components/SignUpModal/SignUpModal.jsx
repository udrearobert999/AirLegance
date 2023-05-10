import * as React from 'react';

import {
  Avatar,
  Button,
  TextField,
  FormControlLabel,
  Checkbox,
  Link,
  Grid,
  Box,
  Typography,
  Container,
  Backdrop,
  Modal,
  Fade,
} from '@mui/material';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';

import Style from './SignUpModal.module.css';

import axios from 'axios';

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

export default function SignUpModal({ open, handleClose }) {
  const [errors, setErrors] = React.useState({});

  const handleSubmit = async (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    const userData = {
      firstName: data.get('firstName'),
      lastName: data.get('lastName'),
      email: data.get('email'),
      password: data.get('password'),
    };

    try {
      const response = await axios.post(
        'http://localhost:9200/api/auth/register',
        userData
      );

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
                Sign up
              </Typography>
              <Box
                component='form'
                className={Style.formMarginTop}
                noValidate
                onSubmit={handleSubmit}
              >
                <Grid container spacing={2}>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      autoComplete='given-name'
                      name='firstName'
                      required
                      fullWidth
                      id='firstName'
                      label='First Name'
                      autoFocus
                      error={!!errors.FirstName}
                      helperText={errors.FirstName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      required
                      fullWidth
                      id='lastName'
                      label='Last Name'
                      name='lastName'
                      autoComplete='family-name'
                      error={!!errors.LastName}
                      helperText={errors.LastName}
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <TextField
                      required
                      fullWidth
                      id='email'
                      label='Email Address'
                      name='email'
                      autoComplete='email'
                      error={!!errors.Email}
                      helperText={errors.Email}
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <TextField
                      required
                      fullWidth
                      name='password'
                      label='Password'
                      type='password'
                      id='password'
                      autoComplete='new-password'
                      error={!!errors.Password}
                      helperText={errors.Password}
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <FormControlLabel
                      control={
                        <Checkbox value='allowExtraEmails' color='primary' />
                      }
                      label='I want to receive inspiration, marketing promotions and updates via email.'
                    />
                  </Grid>
                </Grid>
                <Button
                  type='submit'
                  fullWidth
                  variant='contained'
                  className={Style.submitButton}
                >
                  Sign Up
                </Button>
                <Grid container justifyContent='flex-end'>
                  <Grid item>
                    <Link href='#' variant='body2'>
                      Already have an account? Sign in
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
