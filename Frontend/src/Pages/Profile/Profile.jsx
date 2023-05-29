import React, { useState } from 'react';
import { Box, Card, CardContent, Tabs, Tab, Typography } from '@mui/material';

const ProfilePage = ({ user }) => {
  const [value, setValue] = useState(0);

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        marginTop: '5rem',
        width: '100%',
      }}
    >
      <Typography variant='h4' align='center' gutterBottom>
        Welcome, back PLM PLM
      </Typography>
      <Card
        sx={{ minWidth: 275, maxWidth: 800, width: '100%', marginTop: '5rem' }}
      >
        <CardContent>
          <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
            <Tabs
              value={value}
              onChange={handleChange}
              aria-label='Profile tabs'
            >
              <Tab label='Upcoming Trips' />
              <Tab label='Flight History' />
            </Tabs>
          </Box>
          {value === 0 && <Typography>Content for Upcoming Trips</Typography>}
          {value === 1 && <Typography>Content for Flight History</Typography>}
        </CardContent>
      </Card>
    </Box>
  );
};

export default ProfilePage;
