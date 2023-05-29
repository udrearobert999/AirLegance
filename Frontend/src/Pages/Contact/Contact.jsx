import { useState, useRef } from 'react';

import useAuth from 'Hooks/useAuth';

import Style from './Contact.module.css';

import {
  Card,
  CardContent,
  Grid,
  TextField,
  Button,
  Typography,
  CircularProgress,
} from '@mui/material';

import useAxiosWithErrorRedirect from 'Hooks/useAxiosWithErrorRedirect';
import useSnackbar from 'Hooks/useSnackbar';

export default function Contact() {
  const { auth } = useAuth();

  const { openSuccessSnackbar } = useSnackbar();

  const [loading, setLoading] = useState(false);

  const firstName = useRef(null);
  const lastName = useRef(null);
  const email = useRef(null);
  const message = useRef(null);

  const axios = useAxiosWithErrorRedirect();

  const handleFormSubmit = async (event) => {
    event.preventDefault();

    const complaintData = {
      firstName: firstName.current?.value ?? auth.user?.firstName,
      lastName: lastName.current?.value ?? auth.user?.lastName,
      email: email.current?.value ?? auth.user?.email,
      message: message.current?.value ?? 'No message',
    };

    setLoading(true);

    try {
      const response = await axios.post('/complaints', complaintData);

      openSuccessSnackbar('Complaint submitted successfully');
    } catch (error) {
      console.log(error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className='Contact'>
      <Typography
        gutterBottom
        variant='h3'
        align='center'
        sx={{ textAlign: 'center', padding: '20px 5px' }}
      >
        AirLegance Contact Page
      </Typography>
      <Card style={{ maxWidth: 450, margin: '0 auto', padding: '20px 5px' }}>
        <CardContent>
          <Typography gutterBottom variant='h5'>
            Contact Us
          </Typography>
          <Typography
            gutterBottom
            color='textSecondary'
            variant='body2'
            component='p'
            sx={{ padding: '10px 1px' }}
          >
            Fill up the form and our team will get back to you within 24 hours.
          </Typography>
          <form onSubmit={handleFormSubmit}>
            <Grid container spacing={1}>
              {auth.user ? (
                <></>
              ) : (
                <>
                  <Grid xs={12} sm={6} item>
                    <TextField
                      name='firstName'
                      label='First Name'
                      placeholder='Enter First Name'
                      fullWidth
                      required
                      inputRef={firstName}
                    />
                  </Grid>
                  <Grid xs={12} sm={6} item>
                    <TextField
                      name='lastName'
                      label='Last Name'
                      placeholder='Enter Last Name'
                      fullWidth
                      required
                      inputRef={lastName}
                    />
                  </Grid>
                  <Grid xs={12} item>
                    <TextField
                      type='email'
                      label='Email'
                      placeholder='Enter email'
                      fullWidth
                      required
                      inputRef={email}
                    />
                  </Grid>
                </>
              )}

              <Grid xs={12} item>
                <TextField
                  id="complaintMessageBox"
                  label='message'
                  multiline
                  rows={4}
                  placeholder='Type your message here'
                  fullWidth
                  required
                  inputRef={message}
                />
              </Grid>
              <Grid xs={12} item>
                <Button
                  disabled={loading}
                  className={Style.submitButton}
                  type='submit'
                  variant='contained'
                  color='primary'
                  fullWidth
                >
                  {loading ? <CircularProgress size={24} /> : 'Submit'}
                </Button>
              </Grid>
            </Grid>
          </form>
        </CardContent>
      </Card>
    </div>
  );
}
