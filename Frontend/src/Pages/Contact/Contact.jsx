import { useState } from 'react';

import Autocomplete from '@mui/material/Autocomplete';
import Typography from '@mui/material/Typography';
import Style from './Contact.module.css';
import Card from '@mui/material/Card';
import { CardContent } from '@mui/material';
import { Grid } from '@mui/material';
import { TextField } from '@mui/material';
import { Button } from '@mui/material';

export default function Contact() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    message: '',
  });

  // TODO: use axios here
  // TODO: dont think this works

  const handleFormSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await fetch('/api/contact', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        // Handle successful form submission
        console.log('Form submitted successfully');
      } else {
        // Handle form submission failure
        console.log('Form submission failed');
      }
    } catch (error) {
      // Handle error
      console.error('An error occurred during form submission', error);
    }
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevFormData) => ({ ...prevFormData, [name]: value }));
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
              <Grid xs={12} sm={6} item>
                <TextField
                  name='firstName'
                  label='First Name'
                  placeholder='Enter First Name'
                  fullWidth
                  required
                  value={formData.firstName}
                  onChange={handleInputChange}
                />
              </Grid>
              <Grid xs={12} sm={6} item>
                <TextField
                  name='lastName'
                  label='Last Name'
                  placeholder='Enter Last Name'
                  fullWidth
                  required
                  value={formData.lastName}
                  onChange={handleInputChange}
                />
              </Grid>
              <Grid xs={12} item>
                <TextField
                  type='email'
                  label='Email'
                  placeholder='Enter email'
                  fullWidth
                  required
                  value={formData.email}
                  onChange={handleInputChange}
                />
              </Grid>
              <Grid xs={12} item>
                <TextField
                  label='message'
                  multiline
                  rows={4}
                  placeholder='Type your message here'
                  fullWidth
                  required
                  value={formData.message}
                  onChange={handleInputChange}
                />
              </Grid>
              <Grid xs={12} item>
                <Button
                  type='submit'
                  variant='contained'
                  color='primary'
                  fullWidth
                >
                  Submit
                </Button>
              </Grid>
            </Grid>
          </form>
        </CardContent>
      </Card>
    </div>
  );
}
